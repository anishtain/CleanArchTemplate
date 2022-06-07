namespace Clean.Test.UnitTest.DomainTests
{
    public class Tests
    {
        private int _permissionCount;
        [SetUp]
        public void Setup()
        {
            _permissionCount = 0;
        }

        [Test]
        public void GetAllPermissions_GetAll()
        {
            //Assign
            var permissions = new Domain.Resources.Permissions.Commons.AllPermissions();

            //Align
            var allPermission = permissions?.Permissions;

            //Assert
            Assert.IsTrue(allPermission.Any() && allPermission.Count == _permissionCount);
        }
    }
}