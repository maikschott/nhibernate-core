using System;
using System.Data;
using System.Text;

using NHibernate.Engine;
using NExpression = NHibernate.Expression;
using NHibernate.SqlCommand;
using NHibernate.Type;

using NHibernate.DomainModel;
using NUnit.Framework;

namespace NHibernate.Test.ExpressionTest
{
	/// <summary>
	/// Summary description for NotExpressionFixture.
	/// </summary>
	[TestFixture]
	public class NotExpressionFixture : BaseExpressionFixture
	{
		[Test]
		public void NotSqlStringTest() 
		{
			ISession session = factory.OpenSession();
			
			NExpression.Expression notExpression = NExpression.Expression.Not(NExpression.Expression.Eq("Address", "12 Adress"));

			SqlString sqlString = notExpression.ToSqlString(factoryImpl, typeof(Simple), "simple_alias");

			string expectedSql = "not simple_alias.address = :simple_alias.address";
			
			Parameter firstParam = new Parameter();
			firstParam.SqlType = new SqlTypes.StringSqlType();
			firstParam.TableAlias = "simple_alias";
			firstParam.Name = "address";

			CompareSqlStrings(sqlString, expectedSql, new Parameter[] {firstParam});
			
			session.Close();
		}
		
	}
}
