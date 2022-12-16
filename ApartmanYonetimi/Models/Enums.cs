using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApartmanYonetimi.Models
{
    public class AspNetRolesEnums
    {
        public const string Admin = "1";
        public const string Yonetici = "2";
        public const string Oturan = "3";
        public const string Personel = "4";
    }

    public class PersonTypesEnums
    {
        public const byte Oturan = 1;
        public const byte Personel = 2;
        public const byte Yonetici = 3;
        //public const byte Kiracı = 1;
        //public const byte EvSahibi = 2;
        //public const byte Yonetici = 3;
        //public const byte ApartmanGorevlisi = 4;
        //public const byte TemizlikGorevlisi = 5;
        //public const byte GuvenlikGorevlisi = 6;
        //public const byte Diger = 7;
    }

    public class GeneralTypesEnums
    {
        public const byte Genel = 1;
        public const byte SiteyeOzel = 2;
    }

    public class GeneralPersonTypesEnums
    {
        public const byte Kiracı = 1;
        public const byte EvSahibi = 2;
        public const byte Yonetici = 3;
        public const byte ApartmanGorevlisi = 4;
        public const byte TemizlikGorevlisi = 5;
        public const byte GuvenlikGorevlisi = 6;
        public const byte Diger = 7;
    }

    public class RenevusCollectingStatusEnums
    {
        public const byte Odenmedi= 1;
        public const byte KısmiOdendi = 2;
        public const byte Odendi = 3;
    }

}