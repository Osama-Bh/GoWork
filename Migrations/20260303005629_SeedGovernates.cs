using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class SeedGovernates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [TbGovernates] ON;
INSERT INTO [TbGovernates] ([Id],[CountryId],[Name]) VALUES
(1,122,N'Riyadh'),(2,122,N'Makkah'),(3,122,N'Madinah'),(4,122,N'Eastern Province'),(5,122,N'Asir'),
(6,122,N'Tabuk'),(7,122,N'Hail'),(8,122,N'Northern Borders'),(9,122,N'Jazan'),(10,122,N'Najran'),
(11,122,N'Al Bahah'),(12,122,N'Al Jawf'),(13,122,N'Qassim'),
(14,149,N'Abu Dhabi'),(15,149,N'Dubai'),(16,149,N'Sharjah'),(17,149,N'Ajman'),(18,149,N'Ras Al Khaimah'),
(19,149,N'Fujairah'),(20,149,N'Umm Al Quwain'),
(21,43,N'Cairo'),(22,43,N'Giza'),(23,43,N'Alexandria'),(24,43,N'Qalyubia'),(25,43,N'Dakahlia'),
(26,43,N'Sharqia'),(27,43,N'Gharbia'),(28,43,N'Monufia'),(29,43,N'Beheira'),(30,43,N'Kafr El Sheikh'),
(31,43,N'Damietta'),(32,43,N'Port Said'),(33,43,N'Ismailia'),(34,43,N'Suez'),(35,43,N'South Sinai'),
(36,43,N'North Sinai'),(37,43,N'Fayoum'),(38,43,N'Beni Suef'),(39,43,N'Minya'),(40,43,N'Assiut'),
(41,43,N'Sohag'),(42,43,N'Qena'),(43,43,N'Luxor'),(44,43,N'Aswan'),(45,43,N'Red Sea'),(46,43,N'New Valley'),(47,43,N'Matrouh'),
(48,72,N'Amman'),(49,72,N'Irbid'),(50,72,N'Zarqa'),(51,72,N'Balqa'),(52,72,N'Mafraq'),
(53,72,N'Jerash'),(54,72,N'Ajloun'),(55,72,N'Karak'),(56,72,N'Tafilah'),(57,72,N'Maan'),(58,72,N'Aqaba'),(59,72,N'Madaba'),
(60,75,N'Al Asimah'),(61,75,N'Hawalli'),(62,75,N'Farwaniya'),(63,75,N'Mubarak Al Kabeer'),
(64,75,N'Ahmadi'),(65,75,N'Jahra'),
(66,11,N'Capital'),(67,11,N'Muharraq'),(68,11,N'Northern'),(69,11,N'Southern'),
(70,118,N'Doha'),(71,118,N'Al Rayyan'),(72,118,N'Al Wakrah'),(73,118,N'Al Khor'),(74,118,N'Umm Salal'),
(75,118,N'Al Daayen'),(76,118,N'Al Shamal'),(77,118,N'Al Shahaniya'),
(78,109,N'Muscat'),(79,109,N'Dhofar'),(80,109,N'Musandam'),(81,109,N'Al Buraimi'),(82,109,N'Ad Dakhiliyah'),
(83,109,N'Al Batinah North'),(84,109,N'Al Batinah South'),(85,109,N'Ash Sharqiyah North'),(86,109,N'Ash Sharqiyah South'),
(87,109,N'Ad Dhahirah'),(88,109,N'Al Wusta'),
(89,67,N'Baghdad'),(90,67,N'Basra'),(91,67,N'Nineveh'),(92,67,N'Erbil'),(93,67,N'Sulaymaniyah'),
(94,67,N'Duhok'),(95,67,N'Kirkuk'),(96,67,N'Diyala'),(97,67,N'Anbar'),(98,67,N'Wasit'),
(99,67,N'Najaf'),(100,67,N'Karbala'),(101,67,N'Babil'),(102,67,N'Dhi Qar'),(103,67,N'Maysan'),
(104,67,N'Muthanna'),(105,67,N'Qadisiyyah'),(106,67,N'Saladin'),
(107,156,N'Sanaa'),(108,156,N'Aden'),(109,156,N'Taiz'),(110,156,N'Hodeidah'),(111,156,N'Ibb'),
(112,156,N'Dhamar'),(113,156,N'Hadramaut'),(114,156,N'Hajjah'),(115,156,N'Saada'),(116,156,N'Shabwah'),
(117,156,N'Al Bayda'),(118,156,N'Lahij'),(119,156,N'Marib'),(120,156,N'Amran'),(121,156,N'Al Mahwit'),
(122,156,N'Al Jawf'),(123,156,N'Raymah'),(124,156,N'Abyan'),(125,156,N'Al Dhale''e'),(126,156,N'Al Mahrah'),(127,156,N'Socotra'),
(128,79,N'Beirut'),(129,79,N'Mount Lebanon'),(130,79,N'North Lebanon'),(131,79,N'South Lebanon'),
(132,79,N'Bekaa'),(133,79,N'Nabatieh'),(134,79,N'Akkar'),(135,79,N'Baalbek-Hermel'),
(136,145,N'Istanbul'),(137,145,N'Ankara'),(138,145,N'Izmir'),(139,145,N'Bursa'),(140,145,N'Antalya'),
(141,145,N'Adana'),(142,145,N'Konya'),(143,145,N'Gaziantep'),(144,145,N'Mersin'),(145,145,N'Kayseri'),
(146,64,N'Maharashtra'),(147,64,N'Karnataka'),(148,64,N'Tamil Nadu'),(149,64,N'Delhi'),(150,64,N'Telangana'),
(151,64,N'Uttar Pradesh'),(152,64,N'Gujarat'),(153,64,N'West Bengal'),(154,64,N'Rajasthan'),(155,64,N'Kerala'),
(156,110,N'Punjab'),(157,110,N'Sindh'),(158,110,N'Khyber Pakhtunkhwa'),(159,110,N'Balochistan'),(160,110,N'Islamabad'),
(161,151,N'California'),(162,151,N'Texas'),(163,151,N'New York'),(164,151,N'Florida'),(165,151,N'Illinois'),
(166,151,N'Pennsylvania'),(167,151,N'Ohio'),(168,151,N'Georgia'),(169,151,N'Michigan'),(170,151,N'Washington'),
(171,150,N'England'),(172,150,N'Scotland'),(173,150,N'Wales'),(174,150,N'Northern Ireland'),
(175,54,N'Bavaria'),(176,54,N'Berlin'),(177,54,N'Hamburg'),(178,54,N'Hesse'),(179,54,N'North Rhine-Westphalia'),
(180,50,N'Ile-de-France'),(181,50,N'Provence-Alpes-Cote d''Azur'),(182,50,N'Auvergne-Rhone-Alpes'),(183,50,N'Occitanie'),
(184,27,N'Ontario'),(185,27,N'Quebec'),(186,27,N'British Columbia'),(187,27,N'Alberta'),
(188,8,N'New South Wales'),(189,8,N'Victoria'),(190,8,N'Queensland'),(191,8,N'Western Australia'),
(192,96,N'Casablanca-Settat'),(193,96,N'Rabat-Sale-Kenitra'),(194,96,N'Marrakech-Safi'),(195,96,N'Fes-Meknes'),(196,96,N'Tangier-Tetouan'),
(197,144,N'Tunis'),(198,144,N'Sfax'),(199,144,N'Sousse'),(200,144,N'Gabes'),
(201,3,N'Algiers'),(202,3,N'Oran'),(203,3,N'Constantine'),(204,3,N'Annaba'),(205,3,N'Blida'),
(206,80,N'Tripoli'),(207,80,N'Benghazi'),(208,80,N'Misrata'),
(209,135,N'Khartoum'),(210,135,N'Gezira'),(211,135,N'Red Sea'),
(212,138,N'Damascus'),(213,138,N'Aleppo'),(214,138,N'Homs'),(215,138,N'Latakia'),
(216,111,N'Gaza'),(217,111,N'West Bank'),(218,111,N'Jerusalem'),
(219,129,N'Mogadishu'),(220,129,N'Hargeisa'),
(221,85,N'Kuala Lumpur'),(222,85,N'Selangor'),(223,85,N'Johor'),(224,85,N'Penang'),
(225,71,N'Tokyo'),(226,71,N'Osaka'),(227,71,N'Kyoto'),(228,71,N'Hokkaido'),
(229,131,N'Seoul'),(230,131,N'Busan'),(231,131,N'Incheon'),
(232,30,N'Beijing'),(233,30,N'Shanghai'),(234,30,N'Guangdong'),(235,30,N'Zhejiang'),
(236,20,N'Sao Paulo'),(237,20,N'Rio de Janeiro'),(238,20,N'Minas Gerais'),
(239,126,N'Central Region'),(240,126,N'East Region'),(241,126,N'North Region'),(242,126,N'West Region');
SET IDENTITY_INSERT [TbGovernates] OFF;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
