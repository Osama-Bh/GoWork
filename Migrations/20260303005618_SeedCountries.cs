using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class SeedCountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [TbCountries] ON;
INSERT INTO [TbCountries] ([Id],[Name],[Code],[IsActive]) VALUES
(1,N'Afghanistan','AF',1),(2,N'Albania','AL',1),(3,N'Algeria','DZ',1),(4,N'Andorra','AD',1),(5,N'Angola','AO',1),
(6,N'Argentina','AR',1),(7,N'Armenia','AM',1),(8,N'Australia','AU',1),(9,N'Austria','AT',1),(10,N'Azerbaijan','AZ',1),
(11,N'Bahrain','BH',1),(12,N'Bangladesh','BD',1),(13,N'Belarus','BY',1),(14,N'Belgium','BE',1),(15,N'Benin','BJ',1),
(16,N'Bhutan','BT',1),(17,N'Bolivia','BO',1),(18,N'Bosnia and Herzegovina','BA',1),(19,N'Botswana','BW',1),(20,N'Brazil','BR',1),
(21,N'Brunei','BN',1),(22,N'Bulgaria','BG',1),(23,N'Burkina Faso','BF',1),(24,N'Burundi','BI',1),(25,N'Cambodia','KH',1),
(26,N'Cameroon','CM',1),(27,N'Canada','CA',1),(28,N'Chad','TD',1),(29,N'Chile','CL',1),(30,N'China','CN',1),
(31,N'Colombia','CO',1),(32,N'Comoros','KM',1),(33,N'Congo','CG',1),(34,N'Costa Rica','CR',1),(35,N'Croatia','HR',1),
(36,N'Cuba','CU',1),(37,N'Cyprus','CY',1),(38,N'Czech Republic','CZ',1),(39,N'Denmark','DK',1),(40,N'Djibouti','DJ',1),
(41,N'Dominican Republic','DO',1),(42,N'Ecuador','EC',1),(43,N'Egypt','EG',1),(44,N'El Salvador','SV',1),(45,N'Eritrea','ER',1),
(46,N'Estonia','EE',1),(47,N'Ethiopia','ET',1),(48,N'Fiji','FJ',1),(49,N'Finland','FI',1),(50,N'France','FR',1),
(51,N'Gabon','GA',1),(52,N'Gambia','GM',1),(53,N'Georgia','GE',1),(54,N'Germany','DE',1),(55,N'Ghana','GH',1),
(56,N'Greece','GR',1),(57,N'Guatemala','GT',1),(58,N'Guinea','GN',1),(59,N'Guyana','GY',1),(60,N'Haiti','HT',1),
(61,N'Honduras','HN',1),(62,N'Hungary','HU',1),(63,N'Iceland','IS',1),(64,N'India','IN',1),(65,N'Indonesia','ID',1),
(66,N'Iran','IR',1),(67,N'Iraq','IQ',1),(68,N'Ireland','IE',1),(69,N'Italy','IT',1),(70,N'Jamaica','JM',1),
(71,N'Japan','JP',1),(72,N'Jordan','JO',1),(73,N'Kazakhstan','KZ',1),(74,N'Kenya','KE',1),(75,N'Kuwait','KW',1),
(76,N'Kyrgyzstan','KG',1),(77,N'Laos','LA',1),(78,N'Latvia','LV',1),(79,N'Lebanon','LB',1),(80,N'Libya','LY',1),
(81,N'Lithuania','LT',1),(82,N'Luxembourg','LU',1),(83,N'Madagascar','MG',1),(84,N'Malawi','MW',1),(85,N'Malaysia','MY',1),
(86,N'Maldives','MV',1),(87,N'Mali','ML',1),(88,N'Malta','MT',1),(89,N'Mauritania','MR',1),(90,N'Mauritius','MU',1),
(91,N'Mexico','MX',1),(92,N'Moldova','MD',1),(93,N'Monaco','MC',1),(94,N'Mongolia','MN',1),(95,N'Montenegro','ME',1),
(96,N'Morocco','MA',1),(97,N'Mozambique','MZ',1),(98,N'Myanmar','MM',1),(99,N'Namibia','NA',1),(100,N'Nepal','NP',1),
(101,N'Netherlands','NL',1),(102,N'New Zealand','NZ',1),(103,N'Nicaragua','NI',1),(104,N'Niger','NE',1),(105,N'Nigeria','NG',1),
(106,N'North Korea','KP',1),(107,N'North Macedonia','MK',1),(108,N'Norway','NO',1),(109,N'Oman','OM',1),(110,N'Pakistan','PK',1),
(111,N'Palestine','PS',1),(112,N'Panama','PA',1),(113,N'Paraguay','PY',1),(114,N'Peru','PE',1),(115,N'Philippines','PH',1),
(116,N'Poland','PL',1),(117,N'Portugal','PT',1),(118,N'Qatar','QA',1),(119,N'Romania','RO',1),(120,N'Russia','RU',1),
(121,N'Rwanda','RW',1),(122,N'Saudi Arabia','SA',1),(123,N'Senegal','SN',1),(124,N'Serbia','RS',1),(125,N'Sierra Leone','SL',1),
(126,N'Singapore','SG',1),(127,N'Slovakia','SK',1),(128,N'Slovenia','SI',1),(129,N'Somalia','SO',1),(130,N'South Africa','ZA',1),
(131,N'South Korea','KR',1),(132,N'South Sudan','SS',1),(133,N'Spain','ES',1),(134,N'Sri Lanka','LK',1),(135,N'Sudan','SD',1),
(136,N'Sweden','SE',1),(137,N'Switzerland','CH',1),(138,N'Syria','SY',1),(139,N'Taiwan','TW',1),(140,N'Tajikistan','TJ',1),
(141,N'Tanzania','TZ',1),(142,N'Thailand','TH',1),(143,N'Togo','TG',1),(144,N'Tunisia','TN',1),(145,N'Turkey','TR',1),
(146,N'Turkmenistan','TM',1),(147,N'Uganda','UG',1),(148,N'Ukraine','UA',1),(149,N'United Arab Emirates','AE',1),(150,N'United Kingdom','GB',1),
(151,N'United States','US',1),(152,N'Uruguay','UY',1),(153,N'Uzbekistan','UZ',1),(154,N'Venezuela','VE',1),(155,N'Vietnam','VN',1),
(156,N'Yemen','YE',1),(157,N'Zambia','ZM',1),(158,N'Zimbabwe','ZW',1),(159,N'Bahamas','BS',1),(160,N'Barbados','BB',1),
(161,N'Belize','BZ',1),(162,N'Cape Verde','CV',1),(163,N'Central African Republic','CF',1),(164,N'Congo DR','CD',1),
(165,N'Ivory Coast','CI',1),(166,N'Dominica','DM',1),(167,N'East Timor','TL',1),(168,N'Equatorial Guinea','GQ',1),
(169,N'Grenada','GD',1),(170,N'Guinea-Bissau','GW',1),(171,N'Lesotho','LS',1),(172,N'Liberia','LR',1),
(173,N'Liechtenstein','LI',1),(174,N'Micronesia','FM',1),(175,N'Palau','PW',1),(176,N'Papua New Guinea','PG',1),
(177,N'Saint Lucia','LC',1),(178,N'Samoa','WS',1),(179,N'San Marino','SM',1),(180,N'Sao Tome and Principe','ST',1),
(181,N'Seychelles','SC',1),(182,N'Solomon Islands','SB',1),(183,N'Suriname','SR',1),(184,N'Eswatini','SZ',1),
(185,N'Tonga','TO',1),(186,N'Trinidad and Tobago','TT',1),(187,N'Tuvalu','TV',1),(188,N'Vanuatu','VU',1),
(189,N'Vatican City','VA',1),(190,N'Antigua and Barbuda','AG',1),(191,N'Marshall Islands','MH',1),
(192,N'Nauru','NR',1),(193,N'Kiribati','KI',1),(194,N'Saint Kitts and Nevis','KN',1),(195,N'Saint Vincent','VC',1);
SET IDENTITY_INSERT [TbCountries] OFF;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
