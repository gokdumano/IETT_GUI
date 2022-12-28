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
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;

namespace IETT_GUI
{
    public class ClassesIBB
    {
        private static readonly ClientIBB clientIBB = new ClientIBB();
        private static readonly ClientIBB366 clientIBB366 = new ClientIBB366();
        private static readonly ClientDuyurular clientDuyurular = new ClientDuyurular();
        private static readonly ClientHatDurakGuzergah clientHatDurakGuzergah = new ClientHatDurakGuzergah();

        private static readonly CultureInfo ci = CultureInfo.InvariantCulture;
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
        }

        public class ArsivGorev
        {
            public string ID { get; set; }
            public string NARSIVGOREVID { get; set; }
            public string NKAYITGUNU { get; set; }
            public string SHATKODU { get; set; }
            public string SGUZERGAHKODU { get; set; }
            public string SKAPINUMARA { get; set; }
            public string DTBASLAMAZAMANI { get; set; }
            public string DTBITISZAMANI { get; set; }
            public string SGOREVDURUM { get; set; }
            public string NGOREVID { get; set; }
            public string DTPLANLANANBASLANGICZAMANI { get; set; }
            public string DTDUZENLENENBASLANGICZAMANI { get; set; }
        }
        public class YolcuAdet
        {
            public DateTime Gun { get; set; }
            public string Hat { get; set; }
            public int Yolculuk { get; set; }
        }
        public class Duyuru
        {
            public string Hat { get; set; }
            public string DuyuruTip { get; set; }
            public DateTime DuyuruSaati { get; set; }
            public string Mesaj { get; set; }
        }

        public class Durak
        {
            public string DurakKodu { get; set; }
            public string DurakAdi { get; set; }
            public double XKoordinati { get; set; }
            public double YKoordinati { get; set; }
            public string IlceAdi { get; set; }
            public string Yon { get; set; }
            public string Akilli { get; set; }
            public string Fiziki { get; set; }
            public string DurakTipi { get; set; }
            public string EngelliKullanim { get; set; }
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
            // <NewDataSet xmlns="">
            //   <Table>
            //     <HAT_KODU>29C</HAT_KODU>
            //     <HAT_ADI>TARABYAÜSTÜ - KABATAŞ</HAT_ADI>
            //     <TAM_HAT_ADI>29C/TARABYAÜSTÜ - KABATAŞ</TAM_HAT_ADI>
            //     <HAT_DURUMU>1</HAT_DURUMU>
            //     <BOLGE>Avrupa3</BOLGE>
            //     <SEFER_SURESI>84.77</SEFER_SURESI>
            //   </Table>
            // </NewDataSet>

            XElement xElement = clientIBB.HatServisi_GYY(hat_kodu).Element("Table");
            return new HatServisi
            {
                HatKodu = xElement.Element("HAT_KODU").Value,
                HatAdi = xElement.Element("HAT_ADI").Value,
                TamHatAdi = xElement.Element("TAM_HAT_ADI").Value,
                HatDurumu = xElement.Element("HAT_DURUMU").Value,
                Bolge = xElement.Element("BOLGE").Value,
                SeferSuresi = double.Parse(xElement.Element("SEFER_SURESI").Value, ci)
            };
        }
        public static List<DurakDetay> DurakDetay_GYY(string hat_kodu)
        {
            //<NewDataSet xmlns="">
            //  <Table>
            //    <HATKODU>29C</HATKODU>
            //    <YON>D</YON>
            //    <SIRANO>1</SIRANO>
            //    <DURAKKODU>303381</DURAKKODU>
            //    <DURAKADI>KABATAŞ</DURAKADI>
            //    <XKOORDINATI>28.993189</XKOORDINATI>
            //    <YKOORDINATI>41.034574</YKOORDINATI>
            //    <DURAKTIPI>AÇIK DURAK</DURAKTIPI>
            //    <ISLETMEBOLGE>Avrupa3</ISLETMEBOLGE>
            //    <ISLETMEALTBOLGE>Şişli</ISLETMEALTBOLGE>
            //    <ILCEADI>Beyoglu</ILCEADI>
            //  </Table>
            //  ...
            // </NewDataSet>

            List<DurakDetay> result = new List<DurakDetay>();
            Parallel.ForEach(clientIBB.DurakDetay_GYY(hat_kodu).Elements("Table"), xElement =>
            {
                result.Add(new DurakDetay()
                {
                    HatKodu = xElement.Element("HATKODU").Value,
                    Yon = xElement.Element("YON").Value,
                    SiraNo = int.Parse(xElement.Element("SIRANO").Value, ci),
                    DurakKodu = xElement.Element("DURAKKODU").Value,
                    DurakAdi = xElement.Element("DURAKADI").Value,
                    XKoordinati = double.Parse(xElement.Element("XKOORDINATI").Value, ci),
                    YKoordinati = double.Parse(xElement.Element("YKOORDINATI").Value, ci),
                    DurakTipi = xElement.Element("DURAKTIPI").Value,
                    IsletmeBolge = xElement.Element("ISLETMEBOLGE").Value,
                    IsletmeAltBolge = xElement.Element("ISLETMEALTBOLGE").Value,
                    IlceAdi = xElement.Element("ILCEADI").Value
                });
            });
            return result;
        }
        public static async Task<List<ArsivGorev>> GetArsivGorev(DateTime tarih)
        {
            // <NewDataSet xmlns="">
            //   <Table>
            //     <ID>95770554</ID>
            //     <NARSIVGOREVID>85659560</NARSIVGOREVID>
            //     <NKAYITGUNU>20221215</NKAYITGUNU>
            //     <SHATKODU>AVR1</SHATKODU>
            //     <SGUZERGAHKODU>AVR1_G_D7510</SGUZERGAHKODU>
            //     <SKAPINUMARA>B5018</SKAPINUMARA>
            //     <SGOREVDURUM>T</SGOREVDURUM>
            //     <NGOREVID>89138617</NGOREVID>
            //     <DTPLANLANANBASLANGICZAMANI>2022-12-15T06: 55:00+03:00</DTPLANLANANBASLANGICZAMANI>
            //     <DTDUZENLENENBASLANGICZAMANI>2022-12-15T06: 55:00+03:00</DTDUZENLENENBASLANGICZAMANI>
            //   </Table>
            //   ...
            // </NewDataSet>

            List<ArsivGorev> result = new List<ArsivGorev>();
            await clientIBB366.GetIettArsivGorev_XMLAsync(tarih.ToString("yyyyMMdd")).ContinueWith(t =>
            {
                Parallel.ForEach(t.Result.Body.GetIettArsivGorev_XMLResult.Elements("Table"), xElement =>
                {
                    ArsivGorev arsivGorev = new ArsivGorev();
                    foreach(XElement elem in xElement.Elements())
                    {
                        PropertyInfo propertyInfo = arsivGorev.GetType().GetProperty(elem.Name.LocalName);
                        propertyInfo.SetValue(arsivGorev, Convert.ChangeType(elem.Value, propertyInfo.PropertyType), null);
                    }
                    result.Add(arsivGorev);
                });
            });
            return result;
        }
        public static async Task<List<YolcuAdet>> GetYolcuAdet(DateTime tarih)
        {
            // <NewDataSet xmlns = "">
            //   <Table>
            //     <Gun>2022-12-15T00:00:00+03:00</Gun>
            //     <Hat>34</Hat>
            //     <Yolculuk>775764</Yolculuk>
            //   </Table>
            //   ...
            // </NewDataSet>

            List<YolcuAdet> result = new List<YolcuAdet>();
            await clientIBB366.GetIettYolculukHat_XMLAsync(tarih.ToString("yyyy-MM-dd")).ContinueWith(t =>
            {
                Parallel.ForEach(t.Result.Body.GetIettYolculukHat_XMLResult.Elements("Table"), xElement =>
                {
                    result.Add(new YolcuAdet
                    {
                        Gun = DateTime.ParseExact(xElement.Element("Gun").Value, "yyyy-MM-ddTHH:mm:sszzz", ci),
                        Hat = xElement.Element("Hat") == null ? String.Empty : xElement.Element("Hat").Value,
                        Yolculuk = int.Parse(xElement.Element("Yolculuk").Value),
                    });
                });
            });
            return result;
        }
        public static async Task<List<Duyuru>> GetDuyuru()
        {
            // <NewDataSet xmlns="">
            //   <Table>
            //     <HAT> AVCILAR METROBÜS - SEHIR HASTANESI</HAT>
            //     <TIP>Sefer</TIP>
            //     <GUNCELLEME_SAATI>Kayit Saati: 04:14</GUNCELLEME_SAATI>
            //     <MESAJ>ISTANBUL ÜNIVERSITESI-CERRAHPASA AVCILAR KAMPÜSÜ dan Saat 12:15 de hareket etmesi planlanan seferimiz çesitli nedenlerle yapilamayacaktir.</MESAJ>
            //   </Table>
            //   ...
            // </NewDataSet>

            List<Duyuru> result = new List<Duyuru>();
            await clientDuyurular.GetDuyurular_XMLAsync().ContinueWith(t =>
            {
                Parallel.ForEach(t.Result.Body.GetDuyurular_XMLResult.Elements("Table"), xElement =>
                {
                    result.Add(new Duyuru
                    {
                        Hat = xElement.Element("HAT").Value,
                        DuyuruTip = xElement.Element("TIP").Value,
                        DuyuruSaati = DateTime.ParseExact(xElement.Element("GUNCELLEME_SAATI").Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2], "HH:mm", CultureInfo.InvariantCulture),
                        Mesaj = xElement.Element("MESAJ").Value.Trim()
                    });
                });
            });
            return result;
        }
        public static async Task<List<Durak>> GetDurak(string durakKodu = "")
        {
            //<NewDataSet xmlns="">
            //  <Table>
            //    <SDURAKKODU>225981</SDURAKKODU>
            //    <SDURAKADI>DENİZ HARP OKULU</SDURAKADI>
            //    <KOORDINAT>POINT(29.266183999994 40.8140130005245)</KOORDINAT>
            //    <ILCEADI>Tuzla</ILCEADI>
            //    <SYON>TUZLA</SYON>
            //    <AKILLI>YOK</AKILLI>
            //    <FIZIKI>KAPALI</FIZIKI>
            //    <DURAK_TIPI>CCMODERN</DURAK_TIPI>
            //    <ENGELLIKULLANIM>Uygun Degil</ENGELLIKULLANIM>
            //  </Table>
            //  ...
            // </NewDataSet>

            List<Durak> result = new List<Durak>();
            await clientHatDurakGuzergah.GetDurak_XMLAsync(durakKodu).ContinueWith(t =>
            {
                Parallel.ForEach(t.Result.Body.GetDurak_XMLResult.Elements("Table"), xElement =>
                {
                    result.Add(new Durak
                    {
                        DurakKodu = xElement.Element("SDURAKKODU").Value,
                        DurakAdi = xElement.Element("SDURAKADI").Value,
                        XKoordinati = double.Parse(xElement.Element("KOORDINAT").Value.Split(new[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries)[1], ci),
                        YKoordinati = double.Parse(xElement.Element("KOORDINAT").Value.Split(new[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries)[2], ci),
                        IlceAdi = xElement.Element("ILCEADI").Value,
                        Yon = xElement.Element("SYON").Value,
                        Akilli = xElement.Element("AKILLI").Value,
                        Fiziki = xElement.Element("FIZIKI").Value,
                        DurakTipi = xElement.Element("DURAK_TIPI").Value,
                        EngelliKullanim = xElement.Element("ENGELLIKULLANIM").Value
                    });
                });
            });
            return result;
        }
        public static async Task<List<Garaj>> GetGaraj()
        {
            // <NewDataSet xmlns="">
            //   <Table>
            //     <ID>50002</ID>
            //     <GARAJ_ADI>IKITELLIISLETTIRMEGARAJI</GARAJ_ADI>
            //     <GARAJ_KODU>G_ISL1</GARAJ_KODU>
            //     <KOORDINAT>POINT(28.7915079999927 41.0605889995246)</KOORDINAT>
            //   </Table>
            //   ...
            // </NewDataSet>

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
                        XKoordinati = double.Parse(xElement.Element("KOORDINAT").Value.Split(new[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries)[1], ci),
                        YKoordinati = double.Parse(xElement.Element("KOORDINAT").Value.Split(new[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries)[2], ci)
                    });
                });
            });
            return result;
        }
    }
}
