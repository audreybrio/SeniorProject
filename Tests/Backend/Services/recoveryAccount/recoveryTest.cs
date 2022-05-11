using StudentMultiTool.Backend.Models.RecoveryAccount;
using StudentMultiTool.Backend.Services.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Backend.Services.recoveryAccount
{
    public class recoveryTest
    {
        [Fact]
        public void ResetPassword()
        {
            RecoveryDB r = new RecoveryDB();

            RecoveryPassoward rp = new RecoveryPassoward();
            rp.password = "user123";
            string email = "devarsh.patel@student.csulb.edu";
            bool success = r.sendNewPasswordReset(rp, email);

            Assert.True(success);

        }

        [Fact]
        public void ResetActiavte()
        {
            RecoveryDB r = new RecoveryDB();

            string email = "devarsh.patel@student.csulb.edu";
            bool activate = true;
            bool success = r.UpdateDisableEnabled(email, activate);

            Assert.True(success);

        }

    }
}
