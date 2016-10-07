using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Drawing;

namespace whsc2random
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>


    public partial class MainWindow : Window
    {
        const string FILE1 = "whsc2random.txt";
        List<CPerson> listPerons;
        List<CCity> listCities;
        List<String> listType;
        List<CCompany> listCompanies;
        string COMPANY_TYPE_ALL, COMPANY_TYPE_EACH,     //单位筛选类型
            PRINT_TITLE,PRINT_UNIT, PRINT_TIME, PRINT_PERSON,       //打印:单位、时间、人员
            PRINT_COMPANY, PRINT_POINT,                 //打印:对象、分隔符
            PRINT_PERSON_DATA_WRITE, PRINT_PERSON_WATCH_WRITE;      //打印：制作人、监督人
        Random rand;
        PrintDocument printDocument;
        StringReader lineReader;


        //双击移除已经选定的单位
        private void lbCompanySelected_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //处理未选定任何项就双击的情况
            if (lbCompanySelected.SelectedItem == null) return;
            string s = lbCompanySelected.SelectedItem.ToString();
            foreach (CCompany c in listCompanies)
            {
                if (c.getCompany().Equals(s))
                {
                    //修改单位状态
                    c.setState(STATIC.UNSELETED);
                    break;
                }
            }
            //更新单位数据涉及控件显示
            RefeshListBox(STATIC.LISTBOX_COMPANY);
        }

        //双击添加选定单位
        private void lbCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //处理未选定任何项就双击的情况
            if (lbCompany.SelectedItem == null) return;
            string s = lbCompany.SelectedItem.ToString();
            foreach (CCompany c in listCompanies)
            {
                if (c.getCompany().Equals(s))
                {
                    c.setState(STATIC.SELETED);
                    break;
                }
            }
            RefeshListBox(STATIC.LISTBOX_COMPANY);
        }

        //随机抽取人员
        private void btnGetPerson_Click(object sender, RoutedEventArgs e)
        {
            //移除文本框中非数字字符,防止转换错误
            int countPerson = Convert.ToInt32(delNoNum(txtCountPerson.Text));
            int countPersonSeleted = lbPersonSelected.Items.Count;
            //将修正后字符显示到文本框
            txtCountPerson.Text = countPerson.ToString();
            //随机抽取池初始化
            List<int> iList = new List<int>();
            for(int i = 0;i<listPerons.Count;i++)
            {
                if (listPerons[i].getState().Equals(STATIC.UNSELETED))
                {
                    iList.Add(i);
                }
            }
            //抽取池不为空,且已经抽取数量没到指定数量时,循环抽取
            while (iList.Count > 0 && countPersonSeleted < countPerson)
            {
                //生成范围为0至(Count-1)的随机数
                int index = rand.Next(iList.Count);
                //修改池中对应序号所对应人员列表中序号所对应的人的状态,为选定
                listPerons[iList[index]].setState(STATIC.SELETED);
                countPersonSeleted++;
                //把随机到的从随机池中移除
                iList.RemoveAt(index);
            }
            RefeshListBox(STATIC.LISTBOX_PERSON);
        }

        //随机抽取城市
        private void btnGetCity_Click(object sender, RoutedEventArgs e)
        {
            int countCity = Convert.ToInt32(delNoNum(txtCountCity.Text));
            int countCitySeleted = lbCitySelected.Items.Count;
            txtCountCity.Text = countCity.ToString();
            List<int> iList = new List<int>();
            for (int i = 0; i < listCities.Count; i++)
            {
                if (listCities[i].getState().Equals(STATIC.UNSELETED))
                {
                    iList.Add(i);
                }
            }
            while (iList.Count > 0 && countCitySeleted < countCity)
            {
                int index = rand.Next(iList.Count);
                listCities[iList[index]].setState(STATIC.SELETED);
                countCitySeleted++;
                iList.RemoveAt(index);
            }
            RefeshListBox(STATIC.LISTBOX_CITY);
            RefeshListBox(STATIC.LISTBOX_COMPANY);
        }

        //随机抽取单位
        private void btnGetCompany_Click(object sender, RoutedEventArgs e)
        {
            if (lbCompany.Items.Count <= 0) return;
            int countCompany = Convert.ToInt32(delNoNum(txtCountCompany.Text));
            txtCountCompany.Text = countCompany.ToString();
            string type = cmbTypeOfCompany.SelectedItem.ToString();
            //总是清空已经选定的然后随机抽
            foreach (CCompany c in listCompanies)
            {
                if (c.getState().Equals(STATIC.SELETED)) c.setState(STATIC.UNSELETED);
            }
            //每个城市进行抽取
            foreach (string city in lbCitySelected.Items)
            {
                if(type == COMPANY_TYPE_ALL)            //所有市场随机抽取
                {
                    List<int> iList = new List<int>();
                    //筛选出当前城市所有单位
                    for (int i = 0; i < listCompanies.Count; i++)
                    {
                        if (listCompanies[i].getState().Equals(STATIC.UNSELETED) 
                            && listCompanies[i].getCity().Equals(city))
                        {
                            iList.Add(i);
                        }
                    }
                    int countCompanySeleted = 0;
                    while (iList.Count > 0 && countCompanySeleted < countCompany)
                    {
                        int index = rand.Next(iList.Count);
                        listCompanies[iList[index]].setState(STATIC.SELETED);
                        countCompanySeleted++;
                        iList.RemoveAt(index);
                    }
                }
                else if(type == COMPANY_TYPE_EACH)     //每类市场随机抽取
                {
                    List<int> iList = new List<int>();
                    //筛选出当前城市所有单位
                    for (int i = 0; i < listCompanies.Count; i++)
                    {
                        if (listCompanies[i].getState().Equals(STATIC.UNSELETED)
                            && listCompanies[i].getCity().Equals(city))
                        {
                            iList.Add(i);
                        }
                    }
                    Dictionary<string, int> countCompanyForType = new Dictionary<string, int>();
                    for(int i = 0; i < listType.Count; i++)
                    {
                        countCompanyForType.Add(listType[i], 0);
                    }
                    while (iList.Count > 0)
                    {
                        int index = rand.Next(iList.Count);
                        CCompany tempC = listCompanies[iList[index]];
                        //当单位的类型对应类型计数还不够指定数量时，选定它
                        if (countCompanyForType[tempC.getType()] < countCompany)
                        {
                            tempC.setState(STATIC.SELETED);
                            countCompanyForType[tempC.getType()]++;
                        }


                        //每类都选够数量了，就退出本次随机
                        bool allCount = true;
                        for (int i = 0; i < countCompanyForType.Count; i++)
                        {
                            if (countCompanyForType.ElementAt(i).Value < countCompany)
                                allCount = false;
                        }
                        if (allCount) break;
                        iList.RemoveAt(index);
                    }

                }
                else                                   //单一市场抽取
                {
                    List<int> iList = new List<int>();
                    //筛选出当前城市对应市场类型的单位
                    for (int i = 0; i < listCompanies.Count; i++)
                    {
                        if (listCompanies[i].getState().Equals(STATIC.UNSELETED)
                            && listCompanies[i].getCity().Equals(city)
                            && listCompanies[i].getType().Equals(type))
                        {
                            iList.Add(i);
                        }
                    }
                    int countCompanySeleted = 0;
                    while (iList.Count > 0 && countCompanySeleted < countCompany)
                    {
                        int index = rand.Next(iList.Count);
                        listCompanies[iList[index]].setState(STATIC.SELETED);
                        countCompanySeleted++;
                        iList.RemoveAt(index);
                    }
                }
            }
            RefeshListBox(STATIC.LISTBOX_COMPANY);

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (lbPersonSelected.Items.Count <= 0
                || lbCitySelected.Items.Count <= 0
                || lbCompanySelected.Items.Count <= 0)
                return;

            string dataSave = "";
            dataSave = PRINT_PERSON;
            foreach (string s in lbPersonSelected.Items)
            {
                dataSave = dataSave + s + PRINT_POINT;
            }
            dataSave = dataSave.Substring(0, dataSave.Length - 1);
            rtxDataShow.AppendText(dataSave);
            rtxDataShow.AppendText("\r");

            dataSave = "";
            dataSave = dataSave + PRINT_COMPANY;
            foreach (string s in lbCompanySelected.Items)
            {
                dataSave = dataSave + s + PRINT_POINT;
            }
            dataSave = dataSave.Substring(0, dataSave.Length - 1);
            rtxDataShow.AppendText(dataSave);
            rtxDataShow.AppendText("\r\n");
            foreach (CPerson p in listPerons)
            {
                if (p.getState().Equals(STATIC.SELETED))
                    p.setState(STATIC.SAVED);
            }
            foreach (CCity c in listCities)
            {
                if (c.getState().Equals(STATIC.SELETED))
                {
                    if ((bool)checkBox.IsChecked)
                    {
                        c.setState(STATIC.UNSELETED);
                    }else
                    {
                        c.setState(STATIC.SAVED);
                    }
                }
                    
            }
            foreach (CCompany c in listCompanies)
            {
                if (c.getState().Equals(STATIC.SELETED))
                    c.setState(STATIC.SAVED);
            }
            RefeshListBox(STATIC.LISTBOX_PERSON);
            RefeshListBox(STATIC.LISTBOX_CITY);
            RefeshListBox(STATIC.LISTBOX_COMPANY);
            rtxDataShow.ScrollToEnd();
        }

        private void btnGetOneAndSave_Click(object sender, RoutedEventArgs e)
        {
            if (lbPerson.Items.Count <= 0
                || lbCity.Items.Count <= 0)
                return;
            btnGetPerson_Click(sender, e);
            btnGetCity_Click(sender, e);
            btnGetCompany_Click(sender, e);
            btnSave_Click(sender, e); 
        }

        private void btnGetAllAndSave_Click(object sender, RoutedEventArgs e)
        {
            while(lbPerson.Items.Count > 0 && lbCity.Items.Count > 0)
            {
                btnGetPerson_Click(sender, e);
                btnGetCity_Click(sender, e);
                btnGetCompany_Click(sender, e);
                btnSave_Click(sender, e);
            }
        }

        //市场类型改变时,筛选显示单位列表
        private void cmbTypeOfCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefeshListBox(STATIC.LISTBOX_COMPANY);
        }

        private void cmbTypeOfCompany_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        //双击移除城市的选定
        private void lbCitySelected_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //处理未选定任何项就双击的情况
            if (lbCitySelected.SelectedItem == null) return;
            string s = lbCitySelected.SelectedItem.ToString();
            foreach (CCity c in listCities)
            {
                if (c.getCity().Equals(s))
                {
                    c.setState(STATIC.UNSELETED);
                    break;
                }
            }
            RefeshListBox(STATIC.LISTBOX_CITY);
            //城市选定会影响到单位列表的筛选
            RefeshListBox(STATIC.LISTBOX_COMPANY);
        }

        private void txtCountPerson_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
        }

        private void txtCountPerson_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        //移除字符串中的非0-9字符
        private string delNoNum(string s)
        {
            string newS = "";

            foreach (char c in s)
            {
                if (c >= '0' && c <= '9')
                {
                    newS = newS + c;
                }
            }
            //当为空时设置为0
            if (newS.Equals(""))
            {
                newS = "0";
            }
            return newS;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Init();
            rtxDataShow.Document.Blocks.Clear();
        }

        private void btnPrintPreview_Click(object sender, RoutedEventArgs e)
        {
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;
            lineReader = null;
            try
            {
                printPreviewDialog.ShowDialog();
            }
            catch(Exception excep)
            {
                System.Windows.Forms.MessageBox.Show(excep.Message, "打印出错", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void printDocument_PrintPage(Object sender,PrintPageEventArgs e)
        {
            Font printFont;
            SolidBrush myBrush;

            Graphics g = e.Graphics;
            float linesPerPage = 0;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float bottomMargin = e.MarginBounds.Bottom;
            float rightMargin = e.MarginBounds.Right;
            float yPosition = topMargin;

            string tempStr = mainWin.Title;
            printFont = new Font(new System.Drawing.FontFamily("黑体"), 20);
            myBrush = new SolidBrush(System.Drawing.Color.Black);
            StringFormat sf = new StringFormat();
            g.DrawString(tempStr, printFont, myBrush,
                leftMargin + (e.MarginBounds.Width- g.MeasureString(tempStr, printFont).Width)/2, yPosition,sf);
            yPosition = (float)(yPosition + printFont.GetHeight(g) * 1.5);

            tempStr = PRINT_UNIT + txtMakerName.Text;
            printFont = new Font(new System.Drawing.FontFamily("宋体"), 16);
            g.DrawString(tempStr, printFont, myBrush, leftMargin, yPosition,sf);
            yPosition = (float)(yPosition + printFont.GetHeight(g) * 1.5);

            tempStr = PRINT_TIME + DateTime.Now;
            g.DrawString(tempStr, printFont, myBrush, leftMargin, yPosition, sf);
            yPosition = (float)(yPosition + printFont.GetHeight(g) * 1.5);

            tempStr = PRINT_PERSON_WATCH_WRITE;
            sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            g.DrawString(tempStr, printFont, myBrush, leftMargin, (float)(e.MarginBounds.Bottom- printFont.GetHeight(g) * 1.5), sf);

            tempStr = PRINT_PERSON_DATA_WRITE;
            sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            g.DrawString(tempStr, printFont, myBrush, leftMargin, e.MarginBounds.Bottom, sf);

            if(lineReader == null)
            {
                lineReader = getDataLine(e,printFont);
            }
            string line = null;
            linesPerPage = (e.MarginBounds.Bottom - printFont.GetHeight(g) * 4 - yPosition) / (printFont.GetHeight(g)*1.5f)-1;
            while(count<linesPerPage && ((line = lineReader.ReadLine()) != null))
            {
                if(count != 0 || !line.Equals(""))
                {
                    yPosition = (float)(yPosition + printFont.GetHeight(g) * 1.5);
                    g.DrawString(line, printFont, myBrush, leftMargin, yPosition, sf);
                    count++;
                }

            }
            if(line != null)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                lineReader = null;
            }

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.PrintDialog printDialog = new System.Windows.Forms.PrintDialog();
            printDialog.Document = printDocument;
            lineReader = null;
            if(printDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    printDocument.Print();
                }
                catch (Exception excep)
                {
                    System.Windows.Forms.MessageBox.Show(excep.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    printDocument.PrintController.OnEndPrint(printDocument,new PrintEventArgs());
                }
            }
        }

        private void btnPrintSet_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.PrintDialog printDialog = new System.Windows.Forms.PrintDialog();
            printDialog.Document = printDocument;
            printDialog.ShowDialog();
        }

        private void btnPageSet_Click(object sender, RoutedEventArgs e)
        {
            PageSetupDialog psDialog = new PageSetupDialog();
            psDialog.Document = printDocument;
            psDialog.ShowDialog();
        }

        private StringReader getDataLine(PrintPageEventArgs e, Font f)
        {
            StringReader tempSR = new StringReader(new TextRange(rtxDataShow.Document.ContentStart, rtxDataShow.Document.ContentEnd).Text);
            int lineWidthCount = 0;
            String testStr = "";
            String returnStr = "";
            while((testStr = tempSR.ReadLine()) != null)
            {
                if (testStr.Length > PRINT_PERSON.Length && !returnStr.Equals("")
                    && PRINT_PERSON.Equals(testStr.Substring(0, PRINT_PERSON.Length)))
                {
                    returnStr = returnStr + '\n';
                }
                if(lineWidthCount == 0)
                {
                    lineWidthCount = (int)(e.MarginBounds.Width / (e.Graphics.MeasureString(testStr, f).Width/testStr.Length));
                    STATIC.LOG(lineWidthCount.ToString());
                }
                while(testStr.Length > lineWidthCount)
                {
                    int l = testStr.Length;
                    returnStr = returnStr + testStr.Substring(0, lineWidthCount) + '\n';
                    testStr = testStr.Substring(lineWidthCount, l - lineWidthCount);
                }
                returnStr = returnStr + testStr + '\n';
            }
            return new StringReader(returnStr);
        }

        //双击选定城市
        private void lbCity_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //处理未选定任何项就双击的情况
            if (lbCity.SelectedItem == null) return;
            string s = lbCity.SelectedItem.ToString();
            foreach (CCity c in listCities)
            {
                if (c.getCity().Equals(s))
                {
                    c.setState(STATIC.SELETED);
                    break;
                }
            }
            RefeshListBox(STATIC.LISTBOX_CITY);
            RefeshListBox(STATIC.LISTBOX_COMPANY);
        }

        //双击移除人员选定
        private void lbPersonSelected_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //处理未选定任何项就双击的情况
            if (lbPersonSelected.SelectedItem == null) return;
            string s = lbPersonSelected.SelectedItem.ToString();
            foreach (CPerson p in listPerons)
            {
                if (p.getName().Equals(s))
                {
                    p.setState(STATIC.UNSELETED);
                    break;
                }
            }
            RefeshListBox(STATIC.LISTBOX_PERSON);
        }

        //双击添加人员选定
        private void lbPerson_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //处理未选定任何项就双击的情况
            if (lbPerson.SelectedItem == null) return;
            string s = lbPerson.SelectedItem.ToString();
            foreach(CPerson p in listPerons)
            {
                if (p.getName().Equals(s))
                {
                    p.setState(STATIC.SELETED);
                    break;
                }
            }
            RefeshListBox(STATIC.LISTBOX_PERSON);
        }

        public MainWindow()
        {
            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(this.printDocument_PrintPage);
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            rand = new Random();
            Init();
        }

        //数据和控件初始化
        private void Init()
        {
            listPerons = new List<CPerson>();
            listCities = new List<CCity>();
            listType = new List<string>();
            listCompanies = new List<CCompany>();

            //读取文件
            string[] buffer;
            if (!File.Exists(FILE1))
            {
                System.Windows.MessageBox.Show("缺少关键数据文件，程序无法正常运行");
                this.Close();
            }
            buffer = File.ReadAllLines(FILE1);

            //处理文件
            //读取状态,初始0读取程序配置文本,1读取人员信息,2读取单位信息
            int readState = 0;
            const int READ_STATE_PERSON = 1;
            const int READ_STATE_COMPANY = 2;
            foreach (string s in buffer)
            {
                //注释行和空行不处理
                if (!s.Substring(0, 1).Equals("-") && !s.Equals(""))
                {
                    string[] cfg = s.Split(',');
                    switch (cfg[0])
                    {
                        case "WindowsName":
                            mainWin.Title = cfg[1];
                            break;
                        case "P1C1":
                            label3_Copy3.Content = cfg[1];
                            break;
                        case "P1C2":
                            label.Content = cfg[1];
                            break;
                        case "P1C3":
                            btnGetPerson.Content = cfg[1];
                            break;
                        case "P2C1":
                            label3_Copy4.Content = cfg[1];
                            break;
                        case "P2C2":
                            label1.Content = cfg[1];
                            break;
                        case "P2C3":
                            btnGetCity.Content = cfg[1];
                            break;
                        case "P3C1":
                            label3_Copy5.Content = cfg[1];
                            break;
                        case "P3C2T1":
                            COMPANY_TYPE_ALL = cfg[1];
                            break;
                        case "P3C2T2":
                            COMPANY_TYPE_EACH = cfg[1];
                            break;
                        case "P3C3":
                            label3.Content = cfg[1];
                            break;
                        case "P3C4":
                            btnGetCompany.Content = cfg[1];
                            break;
                        case "P4C1":
                            btnSave.Content = cfg[1];
                            break;
                        case "P4C2":
                            btnGetOneAndSave.Content = cfg[1];
                            break;
                        case "P4C3":
                            btnGetAllAndSave.Content = cfg[1];
                            break;
                        case "P5C1":
                            label3_Copy6.Content = cfg[1];
                            break;
                        case "P5C2":
                            label3_Copy1.Content = cfg[1];
                            break;
                        case "P5C3":
                            label3_Copy2.Content = cfg[1];
                            break;
                        case "P5C4":
                            btnPrintPreview.Content = cfg[1];
                            break;
                        case "P5C5":
                            btnReset.Content = cfg[1];
                            break;
                        case "P5T1":
                            txtMakerName.Text = cfg[1];
                            break;
                        case "P6T1":
                            PRINT_UNIT = cfg[1];
                            break;
                        case "P6T2":
                            PRINT_TIME = cfg[1];
                            break;
                        case "P6T3":
                            PRINT_PERSON = cfg[1];
                            break;
                        case "P6T4":
                            PRINT_COMPANY = cfg[1];
                            break;
                        case "P6T5":
                            PRINT_POINT = cfg[1];
                            break;
                        case "P6T6":
                            PRINT_PERSON_DATA_WRITE = cfg[1];
                            break;
                        case "P6T7":
                            PRINT_PERSON_WATCH_WRITE = cfg[1];
                            break;
                        //遇到指定内容类型标签时,设置不同读取内容
                        case "[Persons]":
                            readState = READ_STATE_PERSON;
                            break;
                        case "[Companies]":
                            readState = READ_STATE_COMPANY;
                            break;
                        default:
                            //当非注释且非文本标签ID且非内容类型标签时,总会来到这里
                            //读取人员信息
                            if (readState == READ_STATE_PERSON)
                            {
                                listPerons.Add(new CPerson(s));

                            }
                            //读取单位信息
                            else if (readState == READ_STATE_COMPANY)
                            {
                                string[] company = s.Split(',');
                                listCompanies.Add(new CCompany(company[0], company[1], company[2]));
                            }
                            break;
                    }
                }

            }
            //根据单位信息统计城市和类型
            foreach (CCompany c in listCompanies)
            {
                //当城市列表不为空时,对比下,不重复添加
                if (listCities.Count > 0)
                {
                    bool isInList = false;
                    foreach (CCity city in listCities)
                    {
                        if (city.getCity().Equals(c.getCity()))
                        {
                            isInList = true;
                        }
                    }
                    //列表中不存在时,才添加
                    if (!isInList)
                    {
                        listCities.Add(new CCity(c.getCity()));
                    }
                }
                //列表为空时,总是添加
                else
                {
                    listCities.Add(new CCity(c.getCity()));
                }
                //当类型列表不为空时,对比下,不重复添加
                if (listType.Count > 0)
                {
                    bool isInList = false;
                    foreach (string t in listType)
                    {
                        if (t.Equals(c.getType()))
                        {
                            isInList = true;
                        }
                    }
                    if (!isInList)
                    {
                        listType.Add(c.getType());
                    }
                }
                //列表为空时,总是添加
                else
                {
                    listType.Add(c.getType());
                }
            }

            //某类数据缺少时,程序无法正常抽取,不执行下去算了.
            if (listPerons.Count <= 0 || listCities.Count <= 0 || listType.Count <= 0 || listCompanies.Count <= 0)
            {
                System.Windows.MessageBox.Show("配置文件中数据出现错误，程序无法继续正常运行！");
                this.Close();
            }

            //初始化类型列表下拉选框
            cmbTypeOfCompany.Items.Clear();
            cmbTypeOfCompany.Items.Add(COMPANY_TYPE_ALL);
            cmbTypeOfCompany.Items.Add(COMPANY_TYPE_EACH);
            foreach (string s in listType)
            {
                cmbTypeOfCompany.Items.Add(s);
            }
            cmbTypeOfCompany.SelectedIndex = 0;

            if(listCities.Count == 1)
            {
                checkBox.IsChecked = true;
                checkBox.IsEnabled = false;
            }

            //更新其他列表的显示
            RefeshListBox(STATIC.LISTBOX_PERSON);
            RefeshListBox(STATIC.LISTBOX_CITY);
            RefeshListBox(STATIC.LISTBOX_COMPANY);


        }

        //更新列表显示
        private void RefeshListBox(int listboxGroup)
        {
            //控制更新不同列表,总是成对更新
            switch (listboxGroup)
            {
                //更新人员列表
                case STATIC.LISTBOX_PERSON:
                    lbPerson.Items.Clear();
                    lbPersonSelected.Items.Clear();
                    foreach (CPerson p in listPerons)
                    {
                        //根据不同状态向不同控件添加数据,下同
                        switch (p.getState())
                        {
                            case STATIC.UNSELETED:
                                lbPerson.Items.Add(p.getName());
                                break;
                            case STATIC.SELETED:
                                lbPersonSelected.Items.Add(p.getName());
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                //更新城市列表
                case STATIC.LISTBOX_CITY:
                    lbCity.Items.Clear();
                    lbCitySelected.Items.Clear();
                    foreach (CCity c in listCities)
                    {
                        switch (c.getState())
                        {
                            case STATIC.UNSELETED:
                                lbCity.Items.Add(c.getCity());
                                break;
                            case STATIC.SELETED:
                                lbCitySelected.Items.Add(c.getCity());
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                //更新单位列表
                case STATIC.LISTBOX_COMPANY:
                    lbCompany.Items.Clear();
                    lbCompanySelected.Items.Clear();
                    RefeshCompanyState();
                    foreach (CCompany c in listCompanies)
                    {
                        switch (c.getState())
                        {
                            case STATIC.UNSELETED:
                                lbCompany.Items.Add(c.getCompany());
                                break;
                            case STATIC.SELETED:
                                lbCompanySelected.Items.Add(c.getCompany());
                                break;
                            default:
                                break;
                        }
                    }
                    break;
            }
        }

        //单位数据更新,其实只是更新状态
        private void RefeshCompanyState()
        {
            if (cmbTypeOfCompany.SelectedItem == null) return;
            string type = cmbTypeOfCompany.SelectedItem.ToString();
            foreach (CCompany c in listCompanies)
            {
                //已经保存的,和已经选定的,不改变状态,只会影响到待选定的单位列表中数据,lbCompany
                if (!c.getState().Equals(STATIC.SAVED) && !c.getState().Equals(STATIC.SELETED))
                {
                    //不管3721，先隐藏掉不显示
                    c.setState(STATIC.HIDED);

                    //根据市场类型筛选
                    bool isType = false;
                    //全部或每类市场时,不对类型做筛选,全显示
                    if (type.Equals(COMPANY_TYPE_ALL) || type.Equals(COMPANY_TYPE_EACH))
                    {
                        isType = true;
                    }
                    //当选定到具体市场时,判断下,当前单位属于该市场类型才显示
                    else if (type.Equals(c.getType()))
                    {
                        isType = true;
                    }

                    //根据城市筛选
                    bool isCity = false;
                    //当有城市被选定时,才显示对应城市数据
                    if (lbCitySelected.Items.Count > 0)
                    {
                        //多个城市被选定时,每个都判断下,只要属于其中1个就显示
                        foreach (string city in lbCitySelected.Items)
                        {
                            if (city.Equals(c.getCity()))
                            {
                                isCity = true;
                                break;
                            }
                        }
                    }

                    //只有类型和城市都符合时,该单位才会被显示
                    if (isType && isCity)
                    {
                        c.setState(STATIC.UNSELETED);
                    }
                }
            }
        }
    }

    public class CPerson
    {
        private int state;
        private string name;
        public CPerson(string _name)
        {
            name = _name;
            //所有数据对象初始状态都是未被选定的,下同
            state = STATIC.UNSELETED;
        }
        public string getName()
        {
            return name;
        }
        public int getState()
        {
            return state;
        }
        public void setState(int _state)
        {
            state = _state;
        }

    }

    public class CCity
    {
        private int state;
        private string city;
        public CCity(string _city)
        {
            city = _city;
            state = STATIC.UNSELETED;
        }
        public string getCity()
        {
            return city;
        }
        public int getState()
        {
            return state;
        }
        public void setState(int _state)
        {
            state = _state;
        }
    }

    public class CCompany
    {
        private int state;
        private string type,city, company;

        public CCompany(string _city,string _type, string _company)
        {
            this.city = _city;
            this.type = _type;
            this.company = _company;
            this.state = STATIC.UNSELETED;
        }
        public string getCity()
        {
            return city;
        }
        public string getType()
        {
            return type;
        }
        public string getCompany()
        {
            return company;
        }
        public int getState()
        {
            return state;
        }
        public void setState(int _state)
        {
            state = _state;
        }
    }

    //一个静态类,用来提供一些静态常量和函数
    public static class STATIC
    {
        public const int LISTBOX_PERSON = 0;
        public const int LISTBOX_CITY = 1;
        public const int LISTBOX_COMPANY = 2;
        public const int HIDED = 0;
        public const int UNSELETED = 1;
        public const int SELETED = 2;
        public const int SAVED = 3;
        public static void LOG(string s)
        {
            Console.WriteLine(s);
        }
    }
}
