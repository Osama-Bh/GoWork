using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class SeedSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [TbSkills] ON;
INSERT INTO [TbSkills] ([Id],[Name]) VALUES
(1,N'JavaScript'),(2,N'Python'),(3,N'Java'),(4,N'C#'),(5,N'C++'),(6,N'TypeScript'),(7,N'PHP'),(8,N'Ruby'),(9,N'Swift'),(10,N'Kotlin'),
(11,N'Go'),(12,N'Rust'),(13,N'Scala'),(14,N'R'),(15,N'MATLAB'),(16,N'Perl'),(17,N'Dart'),(18,N'Lua'),(19,N'Haskell'),(20,N'Elixir'),
(21,N'Clojure'),(22,N'F#'),(23,N'Objective-C'),(24,N'Assembly'),(25,N'COBOL'),(26,N'Fortran'),(27,N'Groovy'),(28,N'Julia'),(29,N'VB.NET'),(30,N'Delphi'),
(31,N'SQL'),(32,N'PL/SQL'),(33,N'T-SQL'),(34,N'Bash'),(35,N'PowerShell'),(36,N'Shell Scripting'),(37,N'ABAP'),(38,N'Solidity'),(39,N'Vyper'),(40,N'Prolog'),
(41,N'Erlang'),(42,N'OCaml'),(43,N'Lisp'),(44,N'Scheme'),(45,N'Ada'),(46,N'Pascal'),(47,N'SAS'),(48,N'VHDL'),(49,N'Verilog'),(50,N'CoffeeScript'),
(51,N'React'),(52,N'Angular'),(53,N'Vue.js'),(54,N'Svelte'),(55,N'Next.js'),(56,N'Nuxt.js'),(57,N'Gatsby'),(58,N'HTML5'),(59,N'CSS3'),(60,N'SASS'),
(61,N'LESS'),(62,N'Tailwind CSS'),(63,N'Bootstrap'),(64,N'Material UI'),(65,N'jQuery'),(66,N'Redux'),(67,N'MobX'),(68,N'Zustand'),(69,N'Webpack'),(70,N'Vite'),
(71,N'Parcel'),(72,N'Rollup'),(73,N'Babel'),(74,N'ESLint'),(75,N'Prettier'),(76,N'Storybook'),(77,N'Web Components'),(78,N'PWA'),(79,N'Responsive Design'),(80,N'Cross-Browser Compatibility'),
(81,N'Accessibility (WCAG)'),(82,N'SEO'),(83,N'Web Performance'),(84,N'GraphQL Client'),(85,N'REST API Integration'),(86,N'WebSocket'),(87,N'Service Workers'),(88,N'IndexedDB'),(89,N'Canvas API'),(90,N'WebGL'),
(91,N'Three.js'),(92,N'D3.js'),(93,N'Chart.js'),(94,N'Leaflet'),(95,N'Mapbox'),(96,N'Electron'),(97,N'Tauri'),(98,N'Astro'),(99,N'Remix'),(100,N'SolidJS'),
(101,N'Node.js'),(102,N'Express.js'),(103,N'NestJS'),(104,N'Django'),(105,N'Flask'),(106,N'FastAPI'),(107,N'Spring Boot'),(108,N'ASP.NET Core'),(109,N'Laravel'),(110,N'Ruby on Rails'),
(111,N'Gin'),(112,N'Echo'),(113,N'Fiber'),(114,N'Actix'),(115,N'Rocket'),(116,N'Phoenix'),(117,N'Koa.js'),(118,N'Fastify'),(119,N'Hapi.js'),(120,N'Strapi'),
(121,N'GraphQL'),(122,N'REST API Design'),(123,N'gRPC'),(124,N'WebSocket Server'),(125,N'Microservices'),(126,N'Serverless'),(127,N'API Gateway'),(128,N'OAuth 2.0'),(129,N'JWT'),(130,N'SAML'),
(131,N'Rate Limiting'),(132,N'Caching Strategies'),(133,N'Message Queues'),(134,N'Event-Driven Architecture'),(135,N'CQRS'),(136,N'Domain-Driven Design'),(137,N'Clean Architecture'),(138,N'Hexagonal Architecture'),(139,N'SOA'),(140,N'Monolith to Microservices'),
(141,N'API Versioning'),(142,N'Swagger/OpenAPI'),(143,N'Postman'),(144,N'Insomnia'),(145,N'Load Balancing'),(146,N'Reverse Proxy'),(147,N'Nginx'),(148,N'Apache'),(149,N'HAProxy'),(150,N'Caddy'),
(151,N'MySQL'),(152,N'PostgreSQL'),(153,N'SQL Server'),(154,N'Oracle DB'),(155,N'MongoDB'),(156,N'Redis'),(157,N'Elasticsearch'),(158,N'Cassandra'),(159,N'DynamoDB'),(160,N'Firebase Realtime DB'),
(161,N'Firestore'),(162,N'CouchDB'),(163,N'Neo4j'),(164,N'InfluxDB'),(165,N'TimescaleDB'),(166,N'MariaDB'),(167,N'SQLite'),(168,N'Supabase'),(169,N'PlanetScale'),(170,N'CockroachDB'),
(171,N'Database Design'),(172,N'Data Modeling'),(173,N'Query Optimization'),(174,N'Indexing Strategies'),(175,N'Stored Procedures'),(176,N'Database Replication'),(177,N'Sharding'),(178,N'ETL'),(179,N'Data Migration'),(180,N'Backup and Recovery'),
(181,N'Entity Framework'),(182,N'Hibernate'),(183,N'Prisma'),(184,N'Sequelize'),(185,N'TypeORM'),(186,N'Drizzle ORM'),(187,N'Mongoose'),(188,N'Dapper'),(189,N'SQLAlchemy'),(190,N'Knex.js'),
(191,N'Database Administration'),(192,N'Performance Tuning'),(193,N'ACID Compliance'),(194,N'CAP Theorem'),(195,N'Data Warehousing'),(196,N'Data Lake'),(197,N'Apache Hive'),(198,N'Apache Spark'),(199,N'Snowflake'),(200,N'BigQuery'),
(201,N'AWS'),(202,N'Azure'),(203,N'Google Cloud Platform'),(204,N'Docker'),(205,N'Kubernetes'),(206,N'Terraform'),(207,N'Ansible'),(208,N'Jenkins'),(209,N'GitHub Actions'),(210,N'GitLab CI/CD'),
(211,N'CircleCI'),(212,N'Travis CI'),(213,N'ArgoCD'),(214,N'Helm'),(215,N'Prometheus'),(216,N'Grafana'),(217,N'ELK Stack'),(218,N'Datadog'),(219,N'New Relic'),(220,N'PagerDuty'),
(221,N'AWS Lambda'),(222,N'AWS EC2'),(223,N'AWS S3'),(224,N'AWS RDS'),(225,N'AWS ECS'),(226,N'AWS EKS'),(227,N'AWS CloudFormation'),(228,N'AWS SQS'),(229,N'AWS SNS'),(230,N'AWS DynamoDB'),
(231,N'Azure DevOps'),(232,N'Azure Functions'),(233,N'Azure App Service'),(234,N'Azure Blob Storage'),(235,N'Google Kubernetes Engine'),(236,N'Cloud Run'),(237,N'Cloud Functions'),(238,N'Firebase'),(239,N'Heroku'),(240,N'Vercel'),
(241,N'Netlify'),(242,N'DigitalOcean'),(243,N'Linux Administration'),(244,N'Windows Server'),(245,N'Networking'),(246,N'DNS Management'),(247,N'SSL/TLS'),(248,N'Firewall Configuration'),(249,N'VPN'),(250,N'Load Testing');
SET IDENTITY_INSERT [TbSkills] OFF;
            ");

            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [TbSkills] ON;
INSERT INTO [TbSkills] ([Id],[Name]) VALUES
(251,N'Chaos Engineering'),(252,N'Site Reliability Engineering'),(253,N'Infrastructure as Code'),(254,N'Configuration Management'),(255,N'Container Orchestration'),(256,N'Service Mesh'),(257,N'Istio'),(258,N'Consul'),(259,N'Vault'),(260,N'Packer'),
(261,N'React Native'),(262,N'Flutter'),(263,N'iOS Development'),(264,N'Android Development'),(265,N'SwiftUI'),(266,N'UIKit'),(267,N'Jetpack Compose'),(268,N'Xamarin'),(269,N'MAUI'),(270,N'Ionic'),
(271,N'Capacitor'),(272,N'Cordova'),(273,N'Expo'),(274,N'App Store Optimization'),(275,N'Google Play Console'),(276,N'TestFlight'),(277,N'Push Notifications'),(278,N'Mobile UI/UX'),(279,N'ARKit'),(280,N'ARCore'),
(281,N'Core Data'),(282,N'Room Database'),(283,N'Realm'),(284,N'Mobile Security'),(285,N'Biometric Authentication'),(286,N'In-App Purchases'),(287,N'Mobile Analytics'),(288,N'Deep Linking'),(289,N'App Performance'),(290,N'Offline-First'),
(291,N'Bluetooth LE'),(292,N'NFC'),(293,N'GPS/Location Services'),(294,N'Camera API'),(295,N'WebRTC'),(296,N'Socket.io'),(297,N'Firebase Cloud Messaging'),(298,N'OneSignal'),(299,N'CodePush'),(300,N'Fastlane'),
(301,N'KMM (Kotlin Multiplatform)'),(302,N'Compose Multiplatform'),(303,N'WatchOS'),(304,N'WearOS'),(305,N'tvOS'),(306,N'Widget Development'),(307,N'App Clips'),(308,N'Instant Apps'),(309,N'Mobile DevOps'),(310,N'App Distribution'),
(311,N'Machine Learning'),(312,N'Deep Learning'),(313,N'Natural Language Processing'),(314,N'Computer Vision'),(315,N'Reinforcement Learning'),(316,N'TensorFlow'),(317,N'PyTorch'),(318,N'Keras'),(319,N'Scikit-learn'),(320,N'Pandas'),
(321,N'NumPy'),(322,N'Matplotlib'),(323,N'Seaborn'),(324,N'Plotly'),(325,N'Jupyter Notebook'),(326,N'Apache Spark MLlib'),(327,N'Hugging Face'),(328,N'OpenAI API'),(329,N'LangChain'),(330,N'RAG'),
(331,N'Prompt Engineering'),(332,N'Fine-Tuning LLMs'),(333,N'GPT'),(334,N'BERT'),(335,N'Transformers'),(336,N'GANs'),(337,N'CNNs'),(338,N'RNNs'),(339,N'LSTMs'),(340,N'Autoencoders'),
(341,N'Feature Engineering'),(342,N'Model Deployment'),(343,N'MLOps'),(344,N'A/B Testing'),(345,N'Statistical Analysis'),(346,N'Hypothesis Testing'),(347,N'Bayesian Statistics'),(348,N'Time Series Analysis'),(349,N'Anomaly Detection'),(350,N'Recommendation Systems'),
(351,N'Data Visualization'),(352,N'Tableau'),(353,N'Power BI'),(354,N'Looker'),(355,N'Apache Airflow'),(356,N'dbt'),(357,N'Data Engineering'),(358,N'Data Pipeline'),(359,N'Stream Processing'),(360,N'Apache Kafka'),
(361,N'Apache Flink'),(362,N'Databricks'),(363,N'MLflow'),(364,N'Kubeflow'),(365,N'ONNX'),(366,N'TensorRT'),(367,N'OpenCV'),(368,N'YOLO'),(369,N'Stable Diffusion'),(370,N'Midjourney'),
(371,N'Speech Recognition'),(372,N'Text-to-Speech'),(373,N'Sentiment Analysis'),(374,N'Named Entity Recognition'),(375,N'Knowledge Graphs'),(376,N'Vector Databases'),(377,N'Pinecone'),(378,N'Weaviate'),(379,N'ChromaDB'),(380,N'FAISS'),
(381,N'Penetration Testing'),(382,N'Vulnerability Assessment'),(383,N'SIEM'),(384,N'SOC Operations'),(385,N'Incident Response'),(386,N'Digital Forensics'),(387,N'Malware Analysis'),(388,N'Network Security'),(389,N'Application Security'),(390,N'Cloud Security'),
(391,N'Identity and Access Management'),(392,N'Zero Trust Architecture'),(393,N'Encryption'),(394,N'PKI'),(395,N'OWASP Top 10'),(396,N'Burp Suite'),(397,N'Metasploit'),(398,N'Nmap'),(399,N'Wireshark'),(400,N'Splunk'),
(401,N'Compliance (ISO 27001)'),(402,N'GDPR'),(403,N'HIPAA'),(404,N'PCI DSS'),(405,N'SOC 2'),(406,N'Risk Assessment'),(407,N'Security Auditing'),(408,N'Threat Modeling'),(409,N'Bug Bounty'),(410,N'Reverse Engineering'),
(411,N'Cryptography'),(412,N'Blockchain Security'),(413,N'Smart Contract Auditing'),(414,N'DevSecOps'),(415,N'Container Security'),(416,N'API Security'),(417,N'WAF'),(418,N'DDoS Mitigation'),(419,N'Endpoint Protection'),(420,N'Security Awareness Training'),
(421,N'UI Design'),(422,N'UX Design'),(423,N'UX Research'),(424,N'Wireframing'),(425,N'Prototyping'),(426,N'Figma'),(427,N'Sketch'),(428,N'Adobe XD'),(429,N'InVision'),(430,N'Framer'),
(431,N'Adobe Photoshop'),(432,N'Adobe Illustrator'),(433,N'Adobe InDesign'),(434,N'Adobe After Effects'),(435,N'Adobe Premiere Pro'),(436,N'Adobe Lightroom'),(437,N'Canva'),(438,N'CorelDRAW'),(439,N'Affinity Designer'),(440,N'Affinity Photo'),
(441,N'Graphic Design'),(442,N'Logo Design'),(443,N'Brand Identity'),(444,N'Typography'),(445,N'Color Theory'),(446,N'Layout Design'),(447,N'Print Design'),(448,N'Packaging Design'),(449,N'Illustration'),(450,N'Icon Design'),
(451,N'Motion Graphics'),(452,N'3D Modeling'),(453,N'Blender'),(454,N'Cinema 4D'),(455,N'Maya'),(456,N'3ds Max'),(457,N'ZBrush'),(458,N'Substance Painter'),(459,N'Unity'),(460,N'Unreal Engine'),
(461,N'Game Design'),(462,N'Level Design'),(463,N'Character Design'),(464,N'Environment Art'),(465,N'Animation'),(466,N'Rigging'),(467,N'Storyboarding'),(468,N'Video Editing'),(469,N'Sound Design'),(470,N'Color Grading'),
(471,N'Design Systems'),(472,N'Atomic Design'),(473,N'User Testing'),(474,N'Usability Testing'),(475,N'Card Sorting'),(476,N'Information Architecture'),(477,N'Interaction Design'),(478,N'Design Thinking'),(479,N'Human-Centered Design'),(480,N'Inclusive Design'),
(481,N'Product Design'),(482,N'Industrial Design'),(483,N'Interior Design'),(484,N'Fashion Design'),(485,N'Textile Design'),(486,N'Jewelry Design'),(487,N'Architectural Visualization'),(488,N'AR/VR Design'),(489,N'Augmented Reality'),(490,N'Virtual Reality'),
(491,N'Agile'),(492,N'Scrum'),(493,N'Kanban'),(494,N'SAFe'),(495,N'Waterfall'),(496,N'PRINCE2'),(497,N'PMP'),(498,N'JIRA'),(499,N'Confluence'),(500,N'Trello');
SET IDENTITY_INSERT [TbSkills] OFF;
            ");

            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [TbSkills] ON;
INSERT INTO [TbSkills] ([Id],[Name]) VALUES
(501,N'Asana'),(502,N'Monday.com'),(503,N'ClickUp'),(504,N'Notion'),(505,N'Linear'),(506,N'Basecamp'),(507,N'Microsoft Project'),(508,N'Gantt Charts'),(509,N'Risk Management'),(510,N'Stakeholder Management'),
(511,N'Budget Management'),(512,N'Resource Planning'),(513,N'Sprint Planning'),(514,N'Retrospectives'),(515,N'Stand-ups'),(516,N'OKRs'),(517,N'KPIs'),(518,N'Roadmap Planning'),(519,N'Release Management'),(520,N'Change Management'),
(521,N'Team Leadership'),(522,N'Conflict Resolution'),(523,N'Negotiation'),(524,N'Presentation Skills'),(525,N'Communication'),(526,N'Critical Thinking'),(527,N'Problem Solving'),(528,N'Decision Making'),(529,N'Time Management'),(530,N'Delegation'),
(531,N'Coaching'),(532,N'Mentoring'),(533,N'Performance Reviews'),(534,N'Hiring'),(535,N'Onboarding'),(536,N'Remote Team Management'),(537,N'Cross-Functional Collaboration'),(538,N'Vendor Management'),(539,N'Contract Negotiation'),(540,N'Procurement'),
(541,N'Digital Marketing'),(542,N'Content Marketing'),(543,N'Social Media Marketing'),(544,N'Email Marketing'),(545,N'SEO Strategy'),(546,N'SEM'),(547,N'Google Ads'),(548,N'Facebook Ads'),(549,N'Instagram Marketing'),(550,N'LinkedIn Marketing'),
(551,N'TikTok Marketing'),(552,N'YouTube Marketing'),(553,N'Influencer Marketing'),(554,N'Affiliate Marketing'),(555,N'Growth Hacking'),(556,N'Marketing Automation'),(557,N'HubSpot'),(558,N'Mailchimp'),(559,N'Salesforce Marketing Cloud'),(560,N'Google Analytics'),
(561,N'Google Tag Manager'),(562,N'Hotjar'),(563,N'Mixpanel'),(564,N'Amplitude'),(565,N'Segment'),(566,N'Copywriting'),(567,N'Content Strategy'),(568,N'Blog Writing'),(569,N'Technical Writing'),(570,N'Ghostwriting'),
(571,N'PR Strategy'),(572,N'Media Relations'),(573,N'Crisis Communication'),(574,N'Brand Strategy'),(575,N'Market Research'),(576,N'Competitive Analysis'),(577,N'Customer Segmentation'),(578,N'Buyer Personas'),(579,N'Customer Journey Mapping'),(580,N'Conversion Rate Optimization'),
(581,N'Landing Page Optimization'),(582,N'Funnel Optimization'),(583,N'Retargeting'),(584,N'Programmatic Advertising'),(585,N'Display Advertising'),(586,N'Native Advertising'),(587,N'Podcast Marketing'),(588,N'Webinar Marketing'),(589,N'Event Marketing'),(590,N'Trade Shows'),
(591,N'Public Speaking'),(592,N'Video Marketing'),(593,N'Viral Marketing'),(594,N'Community Management'),(595,N'Social Listening'),(596,N'Reputation Management'),(597,N'App Store Marketing'),(598,N'Product Marketing'),(599,N'Go-to-Market Strategy'),(600,N'Demand Generation'),
(601,N'B2B Sales'),(602,N'B2C Sales'),(603,N'Inside Sales'),(604,N'Outside Sales'),(605,N'Enterprise Sales'),(606,N'Account Management'),(607,N'Key Account Management'),(608,N'Sales Strategy'),(609,N'Lead Generation'),(610,N'Cold Calling'),
(611,N'Cold Emailing'),(612,N'Sales Funnel'),(613,N'CRM Management'),(614,N'Salesforce CRM'),(615,N'HubSpot CRM'),(616,N'Zoho CRM'),(617,N'Pipeline Management'),(618,N'Sales Forecasting'),(619,N'Revenue Operations'),(620,N'Customer Success'),
(621,N'Customer Retention'),(622,N'Upselling'),(623,N'Cross-Selling'),(624,N'Business Development'),(625,N'Strategic Partnerships'),(626,N'Market Entry Strategy'),(627,N'Business Model Canvas'),(628,N'Lean Startup'),(629,N'Pitch Decks'),(630,N'Fundraising'),
(631,N'Venture Capital'),(632,N'Angel Investment'),(633,N'Financial Modeling'),(634,N'Valuation'),(635,N'Due Diligence'),(636,N'Mergers and Acquisitions'),(637,N'IPO Preparation'),(638,N'Corporate Finance'),(639,N'Investment Banking'),(640,N'Private Equity'),
(641,N'E-Commerce'),(642,N'Shopify'),(643,N'WooCommerce'),(644,N'Magento'),(645,N'Payment Processing'),(646,N'Stripe Integration'),(647,N'PayPal Integration'),(648,N'Supply Chain Management'),(649,N'Logistics'),(650,N'Inventory Management'),
(651,N'Export/Import'),(652,N'International Trade'),(653,N'Customs Regulations'),(654,N'Franchise Management'),(655,N'Retail Management'),(656,N'Visual Merchandising'),(657,N'Customer Experience'),(658,N'Voice of Customer'),(659,N'Net Promoter Score'),(660,N'SaaS Business'),
(661,N'Financial Analysis'),(662,N'Financial Reporting'),(663,N'Budgeting'),(664,N'Forecasting'),(665,N'Cash Flow Management'),(666,N'Cost Accounting'),(667,N'Management Accounting'),(668,N'Tax Planning'),(669,N'Tax Compliance'),(670,N'Audit'),
(671,N'Internal Audit'),(672,N'External Audit'),(673,N'IFRS'),(674,N'GAAP'),(675,N'Bookkeeping'),(676,N'QuickBooks'),(677,N'SAP'),(678,N'Oracle Financials'),(679,N'Xero'),(680,N'FreshBooks'),
(681,N'Accounts Payable'),(682,N'Accounts Receivable'),(683,N'Payroll Processing'),(684,N'Bank Reconciliation'),(685,N'Credit Analysis'),(686,N'Risk Management (Finance)'),(687,N'Treasury Management'),(688,N'Foreign Exchange'),(689,N'Derivatives'),(690,N'Portfolio Management'),
(691,N'Wealth Management'),(692,N'Financial Planning'),(693,N'Estate Planning'),(694,N'Insurance'),(695,N'Actuarial Science'),(696,N'Compliance (Financial)'),(697,N'Anti-Money Laundering'),(698,N'KYC'),(699,N'Regulatory Reporting'),(700,N'FinTech'),
(701,N'Blockchain'),(702,N'Cryptocurrency'),(703,N'DeFi'),(704,N'NFTs'),(705,N'Smart Contracts'),(706,N'Tokenomics'),(707,N'Technical Analysis'),(708,N'Fundamental Analysis'),(709,N'Algorithmic Trading'),(710,N'Quantitative Finance'),
(711,N'ERP Systems'),(712,N'SAP FICO'),(713,N'Oracle EBS'),(714,N'Microsoft Dynamics'),(715,N'Sage'),(716,N'Tally'),(717,N'Cost-Benefit Analysis'),(718,N'ROI Analysis'),(719,N'Break-Even Analysis'),(720,N'Variance Analysis'),
(721,N'Talent Acquisition'),(722,N'Recruitment'),(723,N'Sourcing'),(724,N'Employer Branding'),(725,N'Job Description Writing'),(726,N'Interviewing'),(727,N'Behavioral Interviewing'),(728,N'Assessment Centers'),(729,N'Background Checks'),(730,N'Offer Negotiation'),
(731,N'Employee Onboarding'),(732,N'Training and Development'),(733,N'Learning Management Systems'),(734,N'Performance Management'),(735,N'Succession Planning'),(736,N'Career Development'),(737,N'Compensation and Benefits'),(738,N'Payroll Management'),(739,N'Employee Relations'),(740,N'Labor Law'),
(741,N'Workplace Safety'),(742,N'Diversity and Inclusion'),(743,N'Employee Engagement'),(744,N'Culture Building'),(745,N'HR Analytics'),(746,N'People Analytics'),(747,N'Workforce Planning'),(748,N'Organizational Development'),(749,N'Change Management (HR)'),(750,N'HRIS');
SET IDENTITY_INSERT [TbSkills] OFF;
            ");

            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [TbSkills] ON;
INSERT INTO [TbSkills] ([Id],[Name]) VALUES
(751,N'Workday'),(752,N'BambooHR'),(753,N'ADP'),(754,N'SAP SuccessFactors'),(755,N'Talent Management'),(756,N'Job Evaluation'),(757,N'Competency Modeling'),(758,N'360 Feedback'),(759,N'Exit Interviews'),(760,N'Retention Strategies'),
(761,N'Remote Work Policy'),(762,N'Hybrid Work'),(763,N'Conflict Management'),(764,N'Mediation'),(765,N'Grievance Handling'),(766,N'Union Relations'),(767,N'Immigration and Visa'),(768,N'Expatriate Management'),(769,N'Global HR'),(770,N'HR Compliance'),
(771,N'Mechanical Engineering'),(772,N'Electrical Engineering'),(773,N'Civil Engineering'),(774,N'Chemical Engineering'),(775,N'Structural Engineering'),(776,N'AutoCAD'),(777,N'SolidWorks'),(778,N'CATIA'),(779,N'ANSYS'),(780,N'MATLAB/Simulink'),
(781,N'PLC Programming'),(782,N'SCADA'),(783,N'Industrial Automation'),(784,N'Robotics'),(785,N'IoT'),(786,N'Embedded Systems'),(787,N'Firmware Development'),(788,N'PCB Design'),(789,N'FPGA'),(790,N'Signal Processing'),
(791,N'Control Systems'),(792,N'Power Systems'),(793,N'Renewable Energy'),(794,N'Solar Energy'),(795,N'Wind Energy'),(796,N'HVAC'),(797,N'Plumbing Design'),(798,N'Fire Protection'),(799,N'Building Information Modeling (BIM)'),(800,N'Revit'),
(801,N'Project Management (Construction)'),(802,N'Quality Control'),(803,N'Six Sigma'),(804,N'Lean Manufacturing'),(805,N'Kaizen'),(806,N'5S'),(807,N'ISO 9001'),(808,N'GMP'),(809,N'Supply Chain Optimization'),(810,N'Production Planning'),
(811,N'CNC Programming'),(812,N'3D Printing'),(813,N'Additive Manufacturing'),(814,N'Welding'),(815,N'Machining'),(816,N'Material Science'),(817,N'Metallurgy'),(818,N'Failure Analysis'),(819,N'Non-Destructive Testing'),(820,N'Maintenance Management'),
(821,N'Reliability Engineering'),(822,N'Safety Engineering'),(823,N'Environmental Engineering'),(824,N'Water Treatment'),(825,N'Waste Management'),(826,N'Geotechnical Engineering'),(827,N'Surveying'),(828,N'GIS'),(829,N'Remote Sensing'),(830,N'Mining Engineering'),
(831,N'Clinical Research'),(832,N'Patient Care'),(833,N'Nursing'),(834,N'Pharmacy'),(835,N'Medical Coding'),(836,N'Medical Billing'),(837,N'EMR/EHR Systems'),(838,N'HIPAA Compliance'),(839,N'Telemedicine'),(840,N'Health Informatics'),
(841,N'Epidemiology'),(842,N'Public Health'),(843,N'Biostatistics'),(844,N'Clinical Trials'),(845,N'Regulatory Affairs'),(846,N'FDA Regulations'),(847,N'Drug Development'),(848,N'Pharmacology'),(849,N'Biotechnology'),(850,N'Genomics'),
(851,N'Bioinformatics'),(852,N'Laboratory Management'),(853,N'Pathology'),(854,N'Radiology'),(855,N'Cardiology'),(856,N'Dermatology'),(857,N'Orthopedics'),(858,N'Pediatrics'),(859,N'Surgery'),(860,N'Anesthesiology'),
(861,N'Physical Therapy'),(862,N'Occupational Therapy'),(863,N'Speech Therapy'),(864,N'Nutrition'),(865,N'Dietetics'),(866,N'Mental Health'),(867,N'Psychology'),(868,N'Psychiatry'),(869,N'Counseling'),(870,N'Social Work'),
(871,N'Health Education'),(872,N'Dental'),(873,N'Optometry'),(874,N'Veterinary'),(875,N'Healthcare Administration'),(876,N'Hospital Management'),(877,N'Medical Devices'),(878,N'Biomedical Engineering'),(879,N'Medical Imaging'),(880,N'Infection Control'),
(881,N'Corporate Law'),(882,N'Contract Law'),(883,N'Intellectual Property'),(884,N'Patent Law'),(885,N'Trademark Law'),(886,N'Copyright Law'),(887,N'Employment Law'),(888,N'Immigration Law'),(889,N'Tax Law'),(890,N'Real Estate Law'),
(891,N'Criminal Law'),(892,N'Family Law'),(893,N'International Law'),(894,N'Arbitration'),(895,N'Mediation (Legal)'),(896,N'Compliance'),(897,N'Legal Research'),(898,N'Legal Writing'),(899,N'Contract Drafting'),(900,N'Contract Review'),
(901,N'Due Diligence (Legal)'),(902,N'Litigation'),(903,N'Dispute Resolution'),(904,N'Data Privacy Law'),(905,N'Cyber Law'),(906,N'Banking Law'),(907,N'Securities Law'),(908,N'Antitrust Law'),(909,N'Environmental Law'),(910,N'Construction Law'),
(911,N'Maritime Law'),(912,N'Aviation Law'),(913,N'Sports Law'),(914,N'Entertainment Law'),(915,N'Media Law'),(916,N'Insurance Law'),(917,N'Healthcare Law'),(918,N'Government Contracts'),(919,N'Regulatory Compliance'),(920,N'Legal Tech'),
(921,N'Curriculum Development'),(922,N'Instructional Design'),(923,N'E-Learning'),(924,N'LMS Administration'),(925,N'Moodle'),(926,N'Canvas LMS'),(927,N'Blackboard'),(928,N'SCORM'),(929,N'Articulate Storyline'),(930,N'Adobe Captivate'),
(931,N'Teaching'),(932,N'Tutoring'),(933,N'Classroom Management'),(934,N'Student Assessment'),(935,N'Special Education'),(936,N'Early Childhood Education'),(937,N'Higher Education'),(938,N'Vocational Training'),(939,N'Corporate Training'),(940,N'Workshop Facilitation'),
(941,N'Coaching (Professional)'),(942,N'Executive Coaching'),(943,N'Life Coaching'),(944,N'Career Coaching'),(945,N'Academic Writing'),(946,N'Research Methodology'),(947,N'Thesis Writing'),(948,N'Peer Review'),(949,N'EdTech'),(950,N'Gamification'),
(951,N'Microlearning'),(952,N'Blended Learning'),(953,N'Adaptive Learning'),(954,N'Assessment Design'),(955,N'Rubric Development'),(956,N'Learning Analytics'),(957,N'Educational Psychology'),(958,N'STEM Education'),(959,N'Language Teaching'),(960,N'ESL/TEFL'),
(961,N'Arabic'),(962,N'English'),(963,N'French'),(964,N'Spanish'),(965,N'German'),(966,N'Chinese (Mandarin)'),(967,N'Japanese'),(968,N'Korean'),(969,N'Hindi'),(970,N'Urdu'),
(971,N'Turkish'),(972,N'Persian'),(973,N'Russian'),(974,N'Portuguese'),(975,N'Italian'),(976,N'Dutch'),(977,N'Swedish'),(978,N'Polish'),(979,N'Romanian'),(980,N'Translation'),
(981,N'Interpretation'),(982,N'Localization'),(983,N'Transcription'),(984,N'Subtitling'),(985,N'Voice Over'),(986,N'Dubbing'),(987,N'Proofreading'),(988,N'Editing'),(989,N'Report Writing'),(990,N'Business Writing'),
(991,N'Grant Writing'),(992,N'Proposal Writing'),(993,N'White Papers'),(994,N'Case Studies'),(995,N'Documentation'),(996,N'API Documentation'),(997,N'Knowledge Base'),(998,N'User Manuals'),(999,N'Style Guides'),(1000,N'Brand Voice'),
(1001,N'Manual Testing'),(1002,N'Automated Testing'),(1003,N'Selenium'),(1004,N'Cypress'),(1005,N'Playwright'),(1006,N'Appium'),(1007,N'JUnit'),(1008,N'Jest'),(1009,N'Mocha'),(1010,N'xUnit'),
(1011,N'NUnit'),(1012,N'TestNG'),(1013,N'Cucumber'),(1014,N'BDD'),(1015,N'TDD'),(1016,N'Load Testing (JMeter)'),(1017,N'Performance Testing'),(1018,N'Security Testing'),(1019,N'API Testing'),(1020,N'Regression Testing'),
(1021,N'Integration Testing'),(1022,N'E2E Testing'),(1023,N'Test Planning'),(1024,N'Test Case Design'),(1025,N'Bug Tracking'),(1026,N'Quality Assurance'),(1027,N'Quality Management'),(1028,N'Continuous Testing'),(1029,N'Visual Testing'),(1030,N'Accessibility Testing'),
(1031,N'Git'),(1032,N'GitHub'),(1033,N'GitLab'),(1034,N'Bitbucket'),(1035,N'SVN'),(1036,N'Code Review'),(1037,N'Pair Programming'),(1038,N'Technical Documentation'),(1039,N'Architecture Documentation'),(1040,N'UML'),
(1041,N'Slack'),(1042,N'Microsoft Teams'),(1043,N'Zoom'),(1044,N'Google Workspace'),(1045,N'Microsoft 365'),(1046,N'SharePoint'),(1047,N'Confluence Wiki'),(1048,N'Miro'),(1049,N'FigJam'),(1050,N'Excalidraw');
SET IDENTITY_INSERT [TbSkills] OFF;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
