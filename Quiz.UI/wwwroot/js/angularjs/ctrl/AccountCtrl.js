app.controller("AccountSignInCtrl", AccountSignInCtrl);
app.controller("AccountSignUpCtrl", AccountSignUpCtrl);

function AccountSignInCtrl($scope, $http) {
    $scope.model = {};
    $scope.loading = false;

    $scope.SignIn = function () {
        if (!$scope.loading) {
            $scope.loading = true;
            $http.post(`${apiUrl}/auth/login`, $scope.model).then(response => {
                var resp = response.data;

                if (!resp.isSuccess) {
                    alert(resp.message);
                } else {
                    setToken(resp.data.token);
                    alert('Login successfully');
                    window.location = '/';
                }

                $scope.loading = false;
            });
        }
    }
}

function AccountSignUpCtrl($scope, $http) {
    $scope.model = {};
    $scope.loading = false;

    $scope.SignUp = function () {
        if (!$scope.loading) {
            $scope.loading = true;
            $http.post(`${apiUrl}/auth/register`, $scope.model).then(response => {
                var resp = response.data;

                if (!resp.isSuccess) {
                    alert(resp.message);
                } else {
                    alert(resp.message);
                    window.location = '/account/signin';
                }

                $scope.loading = false;
            });
        }
    }
}