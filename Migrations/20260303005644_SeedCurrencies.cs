using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class SeedCurrencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [TbCurrencies] ON;
INSERT INTO [TbCurrencies] ([Id],[Code],[Name],[IsActive]) VALUES
(1,N'SAR',N'Saudi Riyal',1),(2,N'USD',N'US Dollar',1),(3,N'EUR',N'Euro',1),(4,N'GBP',N'British Pound',1),
(5,N'AED',N'UAE Dirham',1),(6,N'KWD',N'Kuwaiti Dinar',1),(7,N'BHD',N'Bahraini Dinar',1),(8,N'QAR',N'Qatari Riyal',1),
(9,N'OMR',N'Omani Rial',1),(10,N'JOD',N'Jordanian Dinar',1),(11,N'EGP',N'Egyptian Pound',1),(12,N'TRY',N'Turkish Lira',1),
(13,N'JPY',N'Japanese Yen',1),(14,N'CNY',N'Chinese Yuan',1),(15,N'INR',N'Indian Rupee',1),(16,N'PKR',N'Pakistani Rupee',1),
(17,N'BDT',N'Bangladeshi Taka',1),(18,N'MYR',N'Malaysian Ringgit',1),(19,N'SGD',N'Singapore Dollar',1),(20,N'AUD',N'Australian Dollar',1),
(21,N'CAD',N'Canadian Dollar',1),(22,N'CHF',N'Swiss Franc',1),(23,N'SEK',N'Swedish Krona',1),(24,N'NOK',N'Norwegian Krone',1),
(25,N'DKK',N'Danish Krone',1),(26,N'PLN',N'Polish Zloty',1),(27,N'CZK',N'Czech Koruna',1),(28,N'HUF',N'Hungarian Forint',1),
(29,N'RON',N'Romanian Leu',1),(30,N'BGN',N'Bulgarian Lev',1),(31,N'HRK',N'Croatian Kuna',1),(32,N'RUB',N'Russian Ruble',1),
(33,N'UAH',N'Ukrainian Hryvnia',1),(34,N'BRL',N'Brazilian Real',1),(35,N'MXN',N'Mexican Peso',1),(36,N'ARS',N'Argentine Peso',1),
(37,N'CLP',N'Chilean Peso',1),(38,N'COP',N'Colombian Peso',1),(39,N'PEN',N'Peruvian Sol',1),(40,N'ZAR',N'South African Rand',1),
(41,N'NGN',N'Nigerian Naira',1),(42,N'KES',N'Kenyan Shilling',1),(43,N'GHS',N'Ghanaian Cedi',1),(44,N'TZS',N'Tanzanian Shilling',1),
(45,N'UGX',N'Ugandan Shilling',1),(46,N'ETB',N'Ethiopian Birr',1),(47,N'MAD',N'Moroccan Dirham',1),(48,N'TND',N'Tunisian Dinar',1),
(49,N'DZD',N'Algerian Dinar',1),(50,N'LYD',N'Libyan Dinar',1),(51,N'SDG',N'Sudanese Pound',1),(52,N'IQD',N'Iraqi Dinar',1),
(53,N'IRR',N'Iranian Rial',1),(54,N'SYP',N'Syrian Pound',1),(55,N'LBP',N'Lebanese Pound',1),(56,N'YER',N'Yemeni Rial',1),
(57,N'AFN',N'Afghan Afghani',1),(58,N'KZT',N'Kazakhstani Tenge',1),(59,N'UZS',N'Uzbekistani Som',1),(60,N'GEL',N'Georgian Lari',1),
(61,N'AMD',N'Armenian Dram',1),(62,N'AZN',N'Azerbaijani Manat',1),(63,N'THB',N'Thai Baht',1),(64,N'VND',N'Vietnamese Dong',1),
(65,N'PHP',N'Philippine Peso',1),(66,N'IDR',N'Indonesian Rupiah',1),(67,N'KRW',N'South Korean Won',1),(68,N'TWD',N'New Taiwan Dollar',1),
(69,N'HKD',N'Hong Kong Dollar',1),(70,N'NZD',N'New Zealand Dollar',1),(71,N'ILS',N'Israeli Shekel',1),(72,N'KHR',N'Cambodian Riel',1),
(73,N'MMK',N'Myanmar Kyat',1),(74,N'LKR',N'Sri Lankan Rupee',1),(75,N'NPR',N'Nepalese Rupee',1),(76,N'MNT',N'Mongolian Tugrik',1),
(77,N'KGS',N'Kyrgyzstani Som',1),(78,N'TJS',N'Tajikistani Somoni',1),(79,N'TMT',N'Turkmenistani Manat',1),(80,N'BYN',N'Belarusian Ruble',1),
(81,N'ALL',N'Albanian Lek',1),(82,N'BAM',N'Bosnia Mark',1),(83,N'MKD',N'Macedonian Denar',1),(84,N'RSD',N'Serbian Dinar',1),
(85,N'ISK',N'Icelandic Krona',1),(86,N'MDL',N'Moldovan Leu',1),(87,N'CUP',N'Cuban Peso',1),(88,N'JMD',N'Jamaican Dollar',1),
(89,N'TTD',N'Trinidad Dollar',1),(90,N'BBD',N'Barbadian Dollar',1),(91,N'BSD',N'Bahamian Dollar',1),(92,N'BZD',N'Belize Dollar',1),
(93,N'GTQ',N'Guatemalan Quetzal',1),(94,N'HNL',N'Honduran Lempira',1),(95,N'NIO',N'Nicaraguan Cordoba',1),(96,N'PAB',N'Panamanian Balboa',1),
(97,N'DOP',N'Dominican Peso',1),(98,N'HTG',N'Haitian Gourde',1),(99,N'BOB',N'Bolivian Boliviano',1),(100,N'PYG',N'Paraguayan Guarani',1),
(101,N'UYU',N'Uruguayan Peso',1),(102,N'VES',N'Venezuelan Bolivar',1),(103,N'GYD',N'Guyanese Dollar',1),(104,N'SRD',N'Surinamese Dollar',1),
(105,N'FJD',N'Fijian Dollar',1),(106,N'PGK',N'Papua New Guinean Kina',1),(107,N'WST',N'Samoan Tala',1),(108,N'TOP',N'Tongan Paanga',1),
(109,N'VUV',N'Vanuatu Vatu',1),(110,N'SBD',N'Solomon Islands Dollar',1),(111,N'MUR',N'Mauritian Rupee',1),(112,N'SCR',N'Seychellois Rupee',1),
(113,N'MVR',N'Maldivian Rufiyaa',1),(114,N'BND',N'Brunei Dollar',1),(115,N'LAK',N'Lao Kip',1),(116,N'MOP',N'Macanese Pataca',1),
(117,N'BWP',N'Botswana Pula',1),(118,N'NAD',N'Namibian Dollar',1),(119,N'SZL',N'Swazi Lilangeni',1),(120,N'LSL',N'Lesotho Loti',1),
(121,N'MWK',N'Malawian Kwacha',1),(122,N'ZMW',N'Zambian Kwacha',1),(123,N'MZN',N'Mozambican Metical',1),(124,N'RWF',N'Rwandan Franc',1),
(125,N'BIF',N'Burundian Franc',1),(126,N'DJF',N'Djiboutian Franc',1),(127,N'ERN',N'Eritrean Nakfa',1),(128,N'SOS',N'Somali Shilling',1),
(129,N'KMF',N'Comorian Franc',1),(130,N'MGA',N'Malagasy Ariary',1),(131,N'CVE',N'Cape Verdean Escudo',1),(132,N'STN',N'Sao Tome Dobra',1),
(133,N'XOF',N'West African CFA',1),(134,N'XAF',N'Central African CFA',1),(135,N'GMD',N'Gambian Dalasi',1),(136,N'GNF',N'Guinean Franc',1),
(137,N'SLL',N'Sierra Leonean Leone',1),(138,N'LRD',N'Liberian Dollar',1),(139,N'MRU',N'Mauritanian Ouguiya',1),(140,N'CDF',N'Congolese Franc',1),
(141,N'AOA',N'Angolan Kwanza',1),(142,N'SVC',N'Salvadoran Colon',1),(143,N'CRC',N'Costa Rican Colon',1),(144,N'XCD',N'East Caribbean Dollar',1),
(145,N'KPW',N'North Korean Won',1),(146,N'SSP',N'South Sudanese Pound',1),(147,N'ZWL',N'Zimbabwean Dollar',1);
SET IDENTITY_INSERT [TbCurrencies] OFF;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
