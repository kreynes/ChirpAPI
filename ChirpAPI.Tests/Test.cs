using NUnit.Framework;
using System;
using CSIRC;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	public class TestEndpoint : IrcEndpoint
	{
		private StringBuilder _buf;

		public string Sent => _buf.ToString();

		public TestEndpoint()
		{
			_buf = new StringBuilder();
		}

		public Task Send(string rawMessage)
		{
			_buf.Append(rawMessage);
			_buf.Append("\r\n");
			return Task.CompletedTask;
		}
	}
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void TestSendPrivmsg()
		{
			TestEndpoint te = new TestEndpoint();
			te.SendPrivateMessage("Test", "Test test test:::!!");
			Assert.That(te.Sent, Is.EqualTo("PRIVMSG Test :Test test test:::!!\r\n"));
		}
	}
}

