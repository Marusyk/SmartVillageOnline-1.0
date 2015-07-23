﻿using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace Domain
{
    public class CustomSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(AddForeignKeyOperation addForeignKeyOperation)
        {
            addForeignKeyOperation.Name = getFkName(addForeignKeyOperation.PrincipalTable,
                addForeignKeyOperation.DependentTable, addForeignKeyOperation.DependentColumns.ToArray());
            base.Generate(addForeignKeyOperation);
        }

        protected override void Generate(DropForeignKeyOperation dropForeignKeyOperation)
        {
            dropForeignKeyOperation.Name = getFkName(dropForeignKeyOperation.PrincipalTable,
                dropForeignKeyOperation.DependentTable, dropForeignKeyOperation.DependentColumns.ToArray());
            base.Generate(dropForeignKeyOperation);
        }

        protected override void Generate(CreateTableOperation table)
        {
            table.PrimaryKey.Name = getPkName(table.Name);
            base.Generate(table);
        }

        private static string getFkName(string primaryKeyTable, string foreignKeyTable, params string[] foreignTableFields)
        {
            primaryKeyTable = primaryKeyTable.Replace("dbo.", "");
            foreignKeyTable = foreignKeyTable.Replace("dbo.", "");
            return string.Format("{0}_FKC_{1}", primaryKeyTable, foreignKeyTable);
        }

        private static string getPkName(string primaryKeyTable)
        {
            primaryKeyTable = primaryKeyTable.Replace("dbo.", "");
            return "SYS_" + primaryKeyTable + "_PKY";
        }
    }
}
