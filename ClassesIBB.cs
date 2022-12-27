using System.IO;
using System.Linq;
using System.Xml;
using System.Drawing;
using System.Xml.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;

using ClientIBB = IETT_GUI.IBB.ibbSoapClient;
using ClientIBB366 = IETT_GUI.IBB360.ibb360SoapClient;
using ClientDuyurular = IETT_GUI.Duyurular.DuyurularSoapClient;
using ClientHatDurakGuzergah = IETT_GUI.HatDurakGuzergah.WebService1SoapClient;
using System;

namespace IETT_GUI
{
    public class ClassesIBB
    {
        private static ClientIBB clientIBB = new ClientIBB();
        private static ClientIBB366 clientIBB366 = new ClientIBB366();
        private static ClientDuyurular clientDuyurular = new ClientDuyurular();
        private static ClientHatDurakGuzergah clientHatDurakGuzergah = new ClientHatDurakGuzergah();

        private static CultureInfo ci = CultureInfo.InvariantCulture;
        public class Route
        {
            [CsvHelper.Configuration.Attributes.Index(0)]
            public string RouteId { get; set; }
            [CsvHelper.Configuration.Attributes.Index(1)]
            public string AgencyId { get; set; }
            [CsvHelper.Configuration.Attributes.Index(2)]
            public string RouteShortName { get; set; }
            [CsvHelper.Configuration.Attributes.Index(3)]
            public string RouteLongName { get; set; }
            [CsvHelper.Configuration.Attributes.Index(4)]
            public string RouteType { get; set; }
            [CsvHelper.Configuration.Attributes.Index(5)]
            public string RouteDesc { get; set; }
            [CsvHelper.Configuration.Attributes.Index(6)]
            public string RouteCode { get; set; }
        }

        public class Stop
        {
            [CsvHelper.Configuration.Attributes.Index(0)]
            public int StopId { get; set; }
            [CsvHelper.Configuration.Attributes.Index(1)]
            public string StopCode { get; set; }
            [CsvHelper.Configuration.Attributes.Index(2)]
            public string StopName { get; set; }
            [CsvHelper.Configuration.Attributes.Index(3)]
            public string StopDescription { get; set; }
            [CsvHelper.Configuration.Attributes.Index(4)]
            public double StopLatitude { get; set; }
            [CsvHelper.Configuration.Attributes.Index(5)]
            public double StopLongitude { get; set; }
            [CsvHelper.Configuration.Attributes.Index(6)]
            public string StopLocationType { get; set; }
        }

        public class HatServisi
        {
            public string HatKodu { get; set; }
            public string HatAdi { get; set; }
            public string TamHatAdi { get; set; }
            public string HatDurumu { get; set; }
            public string Bolge { get; set; }
            public double SeferSuresi { get; set; }
            public HatServisi(XElement xElement)
            {
                HatKodu = xElement.Element("HAT_KODU").Value;
                HatAdi = xElement.Element("HAT_ADI").Value;
                TamHatAdi = xElement.Element("TAM_HAT_ADI").Value;
                HatDurumu = xElement.Element("HAT_DURUMU").Value;
                Bolge = xElement.Element("BOLGE").Value;
                SeferSuresi = Double.Parse(xElement.Element("SEFER_SURESI").Value, ci);
            }
            public override string ToString()
            {
                return $"HatKodu:{HatKodu}, HatAdi:{HatAdi}, TamHatAdi:{TamHatAdi}, HatDurumu:{HatDurumu}, Bolge:{Bolge}, RouteTravelTime:{SeferSuresi}";
            }
        }

        public class DurakDetay
        {
            public string HatKodu { get; set; }
            public string Yon { get; set; }
            public int SiraNo { get; set; }
            public string DurakKodu { get; set; }
            public string DurakAdi { get; set; }
            public double XKoordinati { get; set; }
            public double YKoordinati { get; set; }
            public string DurakTipi { get; set; }
            public string IsletmeBolge { get; set; }
            public string IsletmeAltBolge { get; set; }
            public string IlceAdi { get; set; }
            public DurakDetay(XElement xElement)
            {
                HatKodu = xElement.Element("HATKODU").Value;
                Yon = xElement.Element("YON").Value;
                SiraNo = int.Parse(xElement.Element("SIRANO").Value, ci);
                DurakKodu = xElement.Element("DURAKKODU").Value;
                DurakAdi = xElement.Element("DURAKADI").Value;
                XKoordinati = double.Parse(xElement.Element("XKOORDINATI").Value, ci);
                YKoordinati = double.Parse(xElement.Element("YKOORDINATI").Value, ci);
                DurakTipi = xElement.Element("DURAKTIPI").Value;
                IsletmeBolge = xElement.Element("ISLETMEBOLGE").Value;
                IsletmeAltBolge = xElement.Element("ISLETMEALTBOLGE").Value;
                IlceAdi = xElement.Element("ILCEADI").Value;
            }
            public override string ToString()
            {
                return $"HatKodu:{HatKodu}, Yon:{Yon}, SiraNo:{SiraNo}, DurakKodu:{DurakKodu}, DurakAdi:{DurakAdi}, XKoordinati:{XKoordinati}, YKoordinati:{YKoordinati}, " +
                    $"DurakTipi:{DurakTipi}, IsletmeBolge:{IsletmeBolge}, IsletmeAltBolge:{IsletmeAltBolge}, IlceAdi:{IlceAdi}";
            }
        }

        public class Garaj
        {
            public string GarajId { get; set; }
            public string GarajAdi { get; set; }
            public string GarajKodu { get; set; }
            public double XKoordinati { get; set; }
            public double YKoordinati { get; set; }
        }

        public static HatServisi HatServisi_GYY(string hat_kodu)
        {
            XElement xElement = clientIBB.HatServisi_GYY(hat_kodu);
            return new HatServisi(xElement.Element("Table"));
        }
        public static IEnumerable<DurakDetay> DurakDetay_GYY(string hat_kodu)
        {
            XElement xElement = clientIBB.DurakDetay_GYY(hat_kodu);
            return xElement.Elements("Table").Select(x => new DurakDetay(x));
        }

        public static async void IETTArsivGorev(DateTime tarih)
        {
            await clientIBB366.GetIettArsivGorev_XMLAsync(tarih.ToString("yyyyMMdd"))
                .ContinueWith(t =>
                {
                    File.WriteAllText($@"C:\Users\deniz\Documents\Visual Studio 2022\Scripts\IETT_GUI\{tarih:yyyy-MM-dd}.xml", t.Result.Body.GetIettArsivGorev_XMLResult.ToString());
                });
        }
        public static async void HatBazliYolcuAdet(DateTime tarih)
        {
            await clientIBB366.GetIettYolculukHat_XMLAsync(tarih.ToString("yyyy-MM-dd"))
                .ContinueWith(t =>
                {
                    File.WriteAllText($@"C:\Users\deniz\Documents\Visual Studio 2022\Scripts\IETT_GUI\{tarih:yyyy-MM-dd}.xml", t.Result.Body.GetIettYolculukHat_XMLResult.ToString());
                });
        }
        public static async void Duyurular()
        {
            await clientDuyurular.GetDuyurular_XMLAsync()
                .ContinueWith(t =>
                {
                    File.WriteAllText(@"C:\Users\deniz\Documents\Visual Studio 2022\Scripts\IETT_GUI\1233.xml", t.Result.Body.GetDuyurular_XMLResult.ToString());
                });
        }

        public static async void GetDurak(string durakKodu = "")
        {
            await clientHatDurakGuzergah.GetDurak_XMLAsync(durakKodu)
                .ContinueWith(t =>
                {
                    File.WriteAllText(@"C:\Users\deniz\Documents\Visual Studio 2022\Scripts\IETT_GUI\GetDurak.xml", t.Result.Body.GetDurak_XMLResult.ToString());
                });
        }

        public static async Task<List<Garaj>> GetGaraj()
        {
            List<Garaj> result = new List<Garaj>();

            await clientHatDurakGuzergah.GetGaraj_XMLAsync().ContinueWith(t =>
            {
                Parallel.ForEach(t.Result.Body.GetGaraj_XMLResult.Elements("Table"), xElement =>
                {
                    result.Add(new Garaj
                    {
                        GarajId = xElement.Element("ID").Value,
                        GarajAdi = xElement.Element("GARAJ_ADI").Value,
                        GarajKodu = xElement.Element("GARAJ_KODU").Value,
                        XKoordinati = Double.Parse(xElement.Element("KOORDINAT").Value.Split(new[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries)[1], ci),
                        YKoordinati = Double.Parse(xElement.Element("KOORDINAT").Value.Split(new[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries)[2], ci)
                    });
                });
            });

            return result;
        }
    }
}
