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
        List<String> listPerons;
        List<String> listCities;
        List<String> listType;
        List<CCompany> listCompanies;
        string COMPANY_TYPE_ALL, COMPANY_TYPE_EACH,PRINT_UNIT, PRINT_TIME, PRINT_PERSON, 
            PRINT_COMPANY, PRINT_POINT, PRINT_PERSON_DATA_WRITE, PRINT_PERSON_WATCH_WRITE;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Init();
        }
        private void Init()
        {
            listPerons = new List<string>();
            listCities = new List<string>();
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
                        listPerons.Add(s);
                    }
                }
                buffer = File.ReadAllLines(FILE3);
                foreach (string s in buffer)
                {
                    if (!s.Equals(""))
                    {
                        string[] company = s.Split(',');
                        listCompanies.Add(new CCompany(listCompanies.Count, company[0], company[1], company[2]));
                    }
                }
                foreach(CCompany c in listCompanies)
                {
                    if (listCities.Count > 0)
                    {
                        bool isInList = false;
                        foreach(string city in listCities)
                        {
                            if (city.Equals(c.getCity()))
                            {
                                isInList = true;
                            }
                        }
                        if (!isInList)
                        {
                            listCities.Add(c.getCity());
                        }
                    }else
                    {
                        listCities.Add(c.getCity());
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
                lbPerson.Items.Clear();
                lbPersonSelected.Items.Clear();
                foreach (string s in listPerons)
                {
                    lbPerson.Items.Add(s);
                }
                lbCity.Items.Clear();
                lbCitySelected.Items.Clear();
                foreach (string s in listCities)
                {
                    lbCity.Items.Add(s);
                }
                lbCompany.Items.Clear();
                lbCompanySelected.Items.Clear();
                foreach (CCompany c in listCompanies)
                {
                    lbCompany.Items.Add(c.getCompany());
                }
                cmbTypeOfCompany.Items.Clear();
                cmbTypeOfCompany.Items.Add(COMPANY_TYPE_ALL);
                cmbTypeOfCompany.Items.Add(COMPANY_TYPE_EACH);
                foreach (string s in listType)
                {
                    cmbTypeOfCompany.Items.Add(s);
                }
                cmbTypeOfCompany.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("缺少关键数据文件，程序无法正常运行");
                this.Close();
            }
            
        }
    }
    public class CCompany
    {
        private int id;
        private string type,city, company;

        public CCompany(int _id, string _city,string _type, string _company)
        {
            this.id = _id;
            this.city = _city;
            this.type = _type;
            this.company = _company;
        }

        public int getId()
        {
            return id;
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
    }
}
