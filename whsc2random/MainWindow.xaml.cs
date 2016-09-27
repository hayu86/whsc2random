using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace whsc2random
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>


    public partial class MainWindow : Window
    {
        const string FILE1 = "whsc2random.cfg";
        const string FILE2 = "执法人员.txt";
        const string FILE3 = "经营单位.txt";
        List<CPerson> listPerons;
        List<CCity> listCities;
        List<String> listType;
        List<CCompany> listCompanies;
        string COMPANY_TYPE_ALL, COMPANY_TYPE_EACH,PRINT_UNIT, PRINT_TIME, PRINT_PERSON, 
            PRINT_COMPANY, PRINT_POINT, PRINT_PERSON_DATA_WRITE, PRINT_PERSON_WATCH_WRITE;
        Random rand;


        private void lbCompanySelected_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string s = lbCompanySelected.SelectedItem.ToString();
            foreach (CCompany c in listCompanies)
            {
                if (c.getCompany().Equals(s))
                {
                    c.setState(STATIC.UNSELETED);
                    break;
                }
            }
            RefeshListBox(STATIC.LISTBOX_COMPANY);
        }

        private void lbCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
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

        private void btnGetPerson_Click(object sender, RoutedEventArgs e)
        {
            int countPerson = Convert.ToInt32(delNoNum(txtCountPerson.Text));
            int countPersonSeleted = 0;
            txtCountPerson.Text = countPerson.ToString();
            List<int> iList = new List<int>();
            for(int i = 0;i<listPerons.Count;i++)
            {
                if (listPerons[i].getState().Equals(STATIC.UNSELETED))
                {
                    iList.Add(i);
                }
            }
            while (iList.Count > 0 && countPersonSeleted < countPerson)
            {
                int index = rand.Next(iList.Count);
                listPerons[iList[index]].setState(STATIC.SELETED);
                countPersonSeleted++;
                iList.RemoveAt(index);
            }
            RefeshListBox(STATIC.LISTBOX_PERSON);
        }

        private void btnGetCity_Click(object sender, RoutedEventArgs e)
        {
            int countCity = Convert.ToInt32(delNoNum(txtCountCity.Text));
            int countCitySeleted = 0;
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

        private void btnGetCompany_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGetOneAndSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGetAllAndSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbTypeOfCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefeshListBox(STATIC.LISTBOX_COMPANY);
        }

        private void cmbTypeOfCompany_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void lbCitySelected_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
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
            RefeshListBox(STATIC.LISTBOX_COMPANY);
        }

        private void txtCountPerson_PreviewKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtCountPerson_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
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
            if (newS.Equals(""))
            {
                newS = "0";
            }
            return newS;
        }

        private void lbCity_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
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

        private void lbPersonSelected_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
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

        private void lbPerson_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
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
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            rand = new Random();
            Init();
        }
        private void Init()
        {
            listPerons = new List<CPerson>();
            listCities = new List<CCity>();
            listType = new List<string>();
            listCompanies = new List<CCompany>();
            string[] buffer;
            if (File.Exists(FILE1) && File.Exists(FILE2) && File.Exists(FILE3))
            {
                buffer = File.ReadAllLines(FILE1);
                foreach(string s in buffer)
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
                            Console.WriteLine(COMPANY_TYPE_ALL + "," + COMPANY_TYPE_EACH);
                            break;
                        case "P3C2T2":
                            COMPANY_TYPE_EACH = cfg[1];
                            Console.WriteLine(COMPANY_TYPE_ALL + "," + COMPANY_TYPE_EACH);
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
                        default:
                            break;
                    }
                }
                buffer = File.ReadAllLines(FILE2);
                foreach(string s in buffer)
                {
                    if (!s.Equals(""))
                    {
                        listPerons.Add(new CPerson(s));
                    }
                }
                buffer = File.ReadAllLines(FILE3);
                foreach (string s in buffer)
                {
                    if (!s.Equals(""))
                    {
                        string[] company = s.Split(',');
                        listCompanies.Add(new CCompany(company[0], company[1], company[2]));
                    }
                }
                foreach(CCompany c in listCompanies)
                {
                    if (listCities.Count > 0)
                    {
                        bool isInList = false;
                        foreach(CCity city in listCities)
                        {
                            if (city.getCity().Equals(c.getCity()))
                            {
                                isInList = true;
                            }
                        }
                        if (!isInList)
                        {
                            listCities.Add(new CCity(c.getCity()));
                        }
                    }else
                    {
                        listCities.Add(new CCity(c.getCity()));
                    }
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
                    else
                    {
                        listType.Add(c.getType());
                    }
                }
                if(listPerons.Count<=0 || listCities.Count <= 0 || listType.Count <= 0 || listCompanies.Count <= 0)
                {
                    MessageBox.Show("某个配置文件中数据出现错误，程序无法继续正常运行！");
                    this.Close();
                }



                cmbTypeOfCompany.Items.Clear();
                cmbTypeOfCompany.Items.Add(COMPANY_TYPE_ALL);
                cmbTypeOfCompany.Items.Add(COMPANY_TYPE_EACH);
                foreach (string s in listType)
                {
                    cmbTypeOfCompany.Items.Add(s);
                }
                cmbTypeOfCompany.SelectedIndex = 0;

                RefeshListBox(STATIC.LISTBOX_PERSON);
                RefeshListBox(STATIC.LISTBOX_CITY);
                RefeshListBox(STATIC.LISTBOX_COMPANY);
            }
            else
            {
                MessageBox.Show("缺少关键数据文件，程序无法正常运行");
                this.Close();
            }
            
        }
        private void RefeshListBox(int listboxGroup)
        {
            switch (listboxGroup)
            {
                case STATIC.LISTBOX_PERSON:
                    lbPerson.Items.Clear();
                    lbPersonSelected.Items.Clear();
                    foreach (CPerson p in listPerons)
                    {
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
        private void RefeshCompanyState()
        {
            string type = cmbTypeOfCompany.SelectedItem.ToString();
            foreach (CCompany c in listCompanies)
            {
                if (!c.getState().Equals(STATIC.SAVED) && !c.getState().Equals(STATIC.SELETED))
                {
                    c.setState(STATIC.HIDED);
                    bool isType = false;
                    if (type.Equals(COMPANY_TYPE_ALL) || type.Equals(COMPANY_TYPE_EACH))
                    {
                        isType = true;
                    }
                    else if (type.Equals(c.getType()))
                    {
                        isType = true;
                    }
                    bool isCity = false;
                    if (lbCitySelected.Items.Count > 0)
                    {
                        foreach (string city in lbCitySelected.Items)
                        {
                            if (city.Equals(c.getCity()))
                            {
                                isCity = true;
                                break;
                            }
                        }
                    }
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
