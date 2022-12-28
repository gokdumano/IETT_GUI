using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using CsvHelper;


namespace IETT_GUI
{
    public partial class MainForm : Form
    {
        public static SaveFileDialog sfd = new SaveFileDialog
        {
            FileName = "gorevler.png",
            Filter = "XML Files (*.xml)|*.xml;*.xml|All files (*.*)|*.*"
        };

        public MainForm()
        {
            InitializeComponent();
            SetupRoutesDataGridView();
            // SetupStopsDataGridView();
        }

        private void SetupRoutesDataGridView()
        {
            string[] RoutesColumnNames = new string[7] { "RouteId", "AgencyId", "RouteShortName", "RouteLongName", "RouteType", "RouteDesc", "RouteCode" };
            DataGridView RoutesDataGridView = new DataGridView 
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                ColumnCount = RoutesColumnNames.Length,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells,
                RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AllowUserToOrderColumns = false,
                AllowUserToResizeColumns = true,
            };
        
            for(int i = 0; i < RoutesColumnNames.Length; i++)
                RoutesDataGridView.Columns[i].Name = RoutesColumnNames[i];
        
            using (var reader = new StreamReader(@"C:\Users\Deniz\Source\Repos\IETT_GUI\routes.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                IEnumerable<ClassesIBB.Route> RoutesRecords = csv.GetRecords<ClassesIBB.Route>();
                foreach (ClassesIBB.Route RoutesRecord in RoutesRecords)
                    RoutesDataGridView.Rows.Add(RoutesRecord.RouteId, RoutesRecord.AgencyId, RoutesRecord.RouteShortName, RoutesRecord.RouteLongName, RoutesRecord.RouteType, RoutesRecord.RouteDesc, RoutesRecord.RouteCode);
            }
            routesContainer.Panel1.Controls.Add(RoutesDataGridView);
        }

        // private void SetupStopsDataGridView()
        // {
        //     string[] StopsColumnNames = new string[7] { "StopId", "StopCode", "StopName", "StopDescription", "StopLatitude", "StopLongitude", "StopLocationType" };
        //     DataGridView StopsDataGridView = new DataGridView
        //     {
        //         Dock = DockStyle.Fill,
        //         AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        //         SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        //         ColumnCount = StopsColumnNames.Length
        //     };
        // 
        //     for (int i = 0; i < StopsColumnNames.Length; i++)
        //         StopsDataGridView.Columns[i].Name = StopsColumnNames[i];
        // 
        //     using (var reader = new StreamReader(@"C:\Users\deniz\Documents\Visual Studio 2022\Scripts\IETT_GUI\stops.csv"))
        //     using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        //     {
        //         IEnumerable<ClassesIBB.Stop> StopRecords = csv.GetRecords<ClassesIBB.Stop>();
        //         foreach (ClassesIBB.Stop StopRecord in StopRecords)
        //             StopsDataGridView.Rows.Add(StopRecord.StopId, StopRecord.StopCode, StopRecord.StopName, StopRecord.StopDescription, StopRecord.StopLatitude, StopRecord.StopLongitude, StopRecord.StopLocationType);
        //     }
        //     splitContainer.Panel2.Controls.Add(StopsDataGridView);
        // }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var names = new[] { "HatKodu", "Yon", "SiraNo", "DurakKodu", "DurakAdi", "XKoordinati", "YKoordinati", "DurakTipi", "IsletmeBolge", "IsletmeAltBolge", "IlceAdi" };

            DataGridView dataGridViewDonus = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                ColumnCount = names.Length,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Aqua,
                    Font = new Font(new DataGridView().DefaultCellStyle.Font.Name, 10, FontStyle.Bold)
                },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells,
                RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AllowUserToOrderColumns = false,
                AllowUserToResizeColumns = true,
            };
            DataGridView dataGridViewGidis = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                ColumnCount = names.Length,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Aqua,
                    Font = new Font(new DataGridView().DefaultCellStyle.Font.Name, 10, FontStyle.Bold)
                },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells,
                RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AllowUserToOrderColumns = false,
                AllowUserToResizeColumns = true,
            };

            for (int i = 0; i < names.Length; i++)
            {
                dataGridViewDonus.Columns[i].Name = names[i];
                dataGridViewGidis.Columns[i].Name = names[i];
            }

            var a = ClassesIBB.DurakDetay_GYY("29C");
            // var b = ClassesIBB.HatServisi_GYY("29C");

            foreach (var gorevArsiv in a)
            {
                if(gorevArsiv != null)
                switch (gorevArsiv.Yon)
                {
                    case "D":
                        {
                            dataGridViewDonus.Rows.Add(gorevArsiv.HatKodu, 
                                                       gorevArsiv.Yon, 
                                                       gorevArsiv.SiraNo, 
                                                       gorevArsiv.DurakKodu, 
                                                       gorevArsiv.DurakAdi,
                                                       gorevArsiv.XKoordinati,
                                                       gorevArsiv.YKoordinati,
                                                       gorevArsiv.DurakTipi,
                                                       gorevArsiv.IsletmeBolge,
                                                       gorevArsiv.IsletmeAltBolge,
                                                       gorevArsiv.IlceAdi);
                            break;
                        }
                    default:
                        {
                            dataGridViewGidis.Rows.Add(gorevArsiv.HatKodu,
                                                       gorevArsiv.Yon,
                                                       gorevArsiv.SiraNo,
                                                       gorevArsiv.DurakKodu,
                                                       gorevArsiv.DurakAdi,
                                                       gorevArsiv.XKoordinati,
                                                       gorevArsiv.YKoordinati,
                                                       gorevArsiv.DurakTipi,
                                                       gorevArsiv.IsletmeBolge,
                                                       gorevArsiv.IsletmeAltBolge,
                                                       gorevArsiv.IlceAdi);
                            break;
                        }
                }

            }
            stopsContainer.Panel1.Controls.Add(dataGridViewDonus);
            stopsContainer.Panel2.Controls.Add(dataGridViewGidis);
        }
    }
}
