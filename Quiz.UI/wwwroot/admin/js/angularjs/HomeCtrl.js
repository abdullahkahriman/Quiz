app.controller("HomeCtrl", HomeCtrl);

function HomeCtrl($scope, $jgHttp) {
    const cName = "dashboard";
    $scope.total = {};
    $scope.wait = true;

    $scope.isLoggedIn = getToken() ? true : false;

    /** Sign out */
    $scope.signOut = function () {
        if (window.confirm("Are you sure?"))
            signOut();
    }

    /** get count */
    function getCount() {
        $jgHttp.getData(`${apiUrl}/${cName}/getCount`, (s => {
            $scope.total = s.data;
            $scope.wait = false;

            setLS("dashboard", JSON.stringify(s.data));
        }), (e => {
            $scope.wait = false;
        }));
    }

    /** initialize */
    function init() {
        const ls = getLS("dashboard");
        if (!ls) {
            getCount();
        } else {
            $scope.total = JSON.parse(ls);
            $scope.wait = false;
        }
    }
    init();
}