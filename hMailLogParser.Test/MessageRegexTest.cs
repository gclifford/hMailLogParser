using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace hMailLogParser.Test
{
    [TestClass]
    public class MessageRegexTest
    {
        [TestMethod]
        public void GetDirection_Sent()
        {
            // Arrange
            string message = "SENT: RCPT TO:<abc@zxy.com>";
            Regex regex = CompiledRegex.SMTPMessage;
            string expected = "SENT";

            // Act
            Match match = regex.Match(message);
            Group actual = match.Groups["Direction"];

            // Assert
            Assert.IsTrue(match.Success, "Match failed.");
            Assert.IsTrue(actual.Success, "Group match failed");
            Assert.AreEqual(expected, actual.Value);
        }

        [TestMethod]
        public void GetDirection_Received()
        {
            // Arrange
            string message = "RECEIVED: 250 sender <abc@zxy> ok";
            Regex regex = CompiledRegex.SMTPMessage;
            string expected = "RECEIVED";

            // Act
            Match match = regex.Match(message);
            Group actual = match.Groups["Direction"];

            // Assert
            Assert.IsTrue(match.Success, "Match failed.");
            Assert.IsTrue(actual.Success, "Group match failed");
            Assert.AreEqual(expected, actual.Value);
        }

        [TestMethod]
        public void GetStatusCode_Valid()
        {
            // Arrange
            string message = "RECEIVED: 250 sender <abc@zxy> ok";
            Regex regex = CompiledRegex.SMTPMessage;
            string expected = "250";

            // Act
            Match match = regex.Match(message);
            Group actual = match.Groups["SMTPStatus"];

            // Assert
            Assert.IsTrue(match.Success, "Match failed.");
            Assert.IsTrue(actual.Success, "Group match failed");
            Assert.AreEqual(expected, actual.Value);
        }

        [TestMethod]
        public void GetStatusCode_InValid_ToShort()
        {
            // Arrange
            string message = "RECEIVED: 25 sender <abc@zxy> ok";
            Regex regex = CompiledRegex.SMTPMessage;

            // Act
            Match match = regex.Match(message);
            Group actual = match.Groups["SMTPStatus"];

            // Assert
            Assert.IsTrue(match.Success, "Match failed.");
            Assert.IsFalse(actual.Success, "Group match failed");
        }

        [TestMethod]
        public void GetStatusCode_CustomSubCode()
        {
            // Arrange
            string message = "RECEIVED: 550-5.1.1 The email account that you tried to reach does not exist. Please try";
            Regex regex = CompiledRegex.SMTPMessage;
            string expected = "550";

            // Act
            Match match = regex.Match(message);
            Group actual = match.Groups["SMTPStatus"];

            // Assert
            Assert.IsTrue(match.Success, "Match failed.");
            Assert.IsTrue(actual.Success, "Group match failed");
            Assert.AreEqual(expected, actual.Value);
        }

        [TestMethod]
        public void GetMessage()
        {
            // Arrange
            string message = "RECEIVED: 250 sender <abc@zxy> ok";
            Regex regex = CompiledRegex.SMTPMessage;
            string expected = "sender <abc@zxy> ok";

            // Act
            Match match = regex.Match(message);
            Group actual = match.Groups["Message"];

            // Assert
            Assert.IsTrue(match.Success, "Match failed.");
            Assert.IsTrue(actual.Success, "Group match failed");
            Assert.AreEqual(expected, actual.Value);
        }

        [TestMethod]
        public void GetMessage_NoStatusCode()
        {
            // Arrange
            string message = "RECEIVED: QUIT";
            Regex regex = CompiledRegex.SMTPMessage;
            string expected = "QUIT";

            // Act
            Match match = regex.Match(message);
            Group actual = match.Groups["Message"];

            // Assert
            Assert.IsTrue(match.Success, "Match failed.");
            Assert.IsTrue(actual.Success, "Group match failed");
            Assert.AreEqual(expected, actual.Value);
        }
    }
}
