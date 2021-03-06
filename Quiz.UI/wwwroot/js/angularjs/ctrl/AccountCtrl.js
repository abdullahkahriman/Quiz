﻿app.controller("AccountSignInCtrl", AccountSignInCtrl);
app.controller("AccountSignUpCtrl", AccountSignUpCtrl);

function AccountSignInCtrl($scope, $http) {
    $scope.model = {};
    $scope.loading = false;

    $scope.SignIn = function () {
        if (!$scope.loading) {
            $scope.loading = true;
            $http.post(`${apiUrl}/auth/login`, $scope.model)
                .then(response => {
                    var resp = response.data;

                    if (!resp.isSuccess) {
                        alert(resp.message);
                    } else {
                        setToken(resp.data.token);
                        window.location = resp.data.go;
                    }

                    $scope.loading = false;
                }).catch(err => {
                    if (err.xhrStatus === "error") {
                        alert("ERROR!");
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
            $http.post(`${apiUrl}/auth/register`, $scope.model)
                .then(response => {
                    var resp = response.data;

                    if (!resp.isSuccess) {
                        alert(resp.message);
                    } else {
                        alert(resp.message);
                        window.location = '/account/signin';
                    }

                    $scope.loading = false;
                }).catch(err => {
                    if (err.xhrStatus === "error") {
                        alert("ERROR!");
                    }

                    $scope.loading = false;
                });
        }
    }
}