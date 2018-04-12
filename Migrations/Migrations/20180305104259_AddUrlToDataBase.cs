using DAL.Model;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DAL.Migrations
{
    public partial class AddUrlToDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (var db = new ModelContext())
            {
                db.Datasources.Add(new DatasourceDbo { TypeOfData = 0, Url = "http://k.img.com.ua/rss/ru/all_news2.0.xml" });
                db.SaveChanges();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
