app.controller("AccountSignInCtrl", AccountSignInCtrl);
app.controller("AccountSignUpCtrl", AccountSignUpCtrl);

function AccountSignInCtrl($scope, $http) {
    $scope.model = {};

    $scope.SignIn = function () {
        $http.post(`${apiUrl}/auth/login`, $scope.model).then(response => {
            var resp = response.data;

            if (!resp.isSuccess) {
                alert(resp.message);
            } else {
                setToken(resp.data.token);
                alert('Login successfully');
                window.location = '/';
            }
        });
    }
}

function AccountSignUpCtrl($scope, $http) {
    $scope.model = {};

    $scope.SignUp = function () {
        $http.post(`${apiUrl}/auth/register`, $scope.model).then(response => {
            var resp = response.data;

            if (!resp.isSuccess) {
                alert(resp.message);
            } else {
                alert(resp.message);
                window.location = '/account/signin';
            }
        });
    }
}