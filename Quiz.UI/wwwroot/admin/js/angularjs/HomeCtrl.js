app.controller("HomeCtrl", HomeCtrl);

function HomeCtrl($scope, $jgHttp) {
    const cName = "dashboard";
    $scope.total = {};
    $scope.wait = true;

    $scope.isLoggedIn = getToken() ? true : false;

    /** Sign out */
    $scope.signOut = function () {
        if (confirm("Are you sure?"))
            signOut();
    }

    function init() {
        const ls = getLS("dashboard");
        if (!ls) {
            $jgHttp.getData(`${apiUrl}/${cName}/getCount`, (s => {
                $scope.total = s.data;
                $scope.wait = false;

                setLS("dashboard", JSON.stringify(s.data));
            }), (e => {
                $scope.wait = false;
            }));
        } else {
            $scope.total = JSON.parse(ls);
            $scope.wait = false;
        }
    }
    init();

}