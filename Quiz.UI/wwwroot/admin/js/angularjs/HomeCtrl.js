app.controller("HomeCtrl", HomeCtrl);

function HomeCtrl($scope) {
    $scope.isLoggedIn = getToken() ? true : false;

    $scope.signOut = function () {
        if (confirm("Are you sure?"))
            signOut();
    }

}