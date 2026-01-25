using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Seeding Categories
            migrationBuilder.InsertData(
                table: "TbCategories",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "Software Development" },
                    { "Web Development" },
                    { "Mobile App Development" },
                    { "Data Science" },
                    { "Artificial Intelligence" },
                    { "Machine Learning" },
                    { "Cyber Security" },
                    { "Cloud Computing" },
                    { "DevOps Engineering" },
                    { "Game Development" },

                    { "UI/UX Design" },
                    { "Graphic Design" },
                    { "Product Design" },
                    { "Motion Graphics" },
                    { "Video Editing" },

                    { "Digital Marketing" },
                    { "Content Writing" },
                    { "SEO & SEM" },
                    { "Social Media Management" },
                    { "Brand Management" },

                    { "Sales" },
                    { "Business Development" },
                    { "Account Management" },
                    { "Customer Support" },
                    { "Call Center" },

                    { "Project Management" },
                    { "Product Management" },
                    { "Operations Management" },
                    { "Supply Chain Management" },
                    { "Logistics" },

                    { "Human Resources" },
                    { "Talent Acquisition" },
                    { "Training & Development" },
                    { "Payroll & Compensation" },

                    { "Finance" },
                    { "Accounting" },
                    { "Auditing" },
                    { "Taxation" },
                    { "Investment Banking" },
                    { "Financial Analysis" },

                    { "Legal Affairs" },
                    { "Corporate Law" },
                    { "Compliance" },
                    { "Risk Management" },

                    { "Healthcare" },
                    { "Nursing" },
                    { "Pharmacy" },
                    { "Medical Laboratory" },
                    { "Public Health" },

                    { "Education" },
                    { "Teaching" },
                    { "Academic Research" },
                    { "E-Learning" },
                    { "Curriculum Development" },

                    { "Engineering - Civil" },
                    { "Engineering - Electrical" },
                    { "Engineering - Mechanical" },
                    { "Engineering - Industrial" },
                    { "Engineering - Architecture" },

                    { "Construction" },
                    { "Real Estate" },
                    { "Property Management" },

                    { "Manufacturing" },
                    { "Quality Assurance" },
                    { "Production Planning" },

                    { "Retail" },
                    { "Wholesale" },
                    { "E-Commerce" },

                    { "Hospitality" },
                    { "Hotel Management" },
                    { "Tourism" },
                    { "Travel & Aviation" },

                    { "Transportation" },
                    { "Fleet Management" },

                    { "Media & Journalism" },
                    { "Public Relations" },
                    { "Broadcasting" },

                    { "Photography" },
                    { "Film Production" },

                    { "Agriculture" },
                    { "Food Production" },
                    { "Food Safety" },

                    { "Environmental Science" },
                    { "Sustainability" },

                    { "Energy & Utilities" },
                    { "Oil & Gas" },
                    { "Renewable Energy" },

                    { "Telecommunications" },
                    { "Network Engineering" },

                    { "Government & Public Sector" },
                    { "Non-Profit Organizations" },

                    { "Security Services" },
                    { "Facility Management" },

                    { "Fashion Design" },
                    { "Textile Industry" },

                    { "Sports Management" },
                    { "Fitness & Wellness" },

                    { "Research & Development" },
                    { "Innovation Management" },
                    { "General Administration" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM TbCategories");
        }
    }
}
