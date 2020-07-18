app.controller("SystemActionListCtrl", SystemActionListCtrl);
app.controller("SystemActionCreateCtrl", SystemActionCreateCtrl);

/**
 * SystemAction List Ctrl
 * @param {any} $scope
 * @param {any} $jgHttp
 */
function SystemActionListCtrl($scope, $jgHttp) {
    const cName = "systemaction";
    $scope.wait = true;
    $scope.systemActionList = [];

    function init() {
        $jgHttp.getData(`${apiUrl}/${cName}/get`, (s => {
            $scope.systemActionList = s.data;
            $scope.wait = false;
        }), (e => {
            $scope.wait = false;
        }));
    }
    init();
}

/**
 * SystemAction Create Ctrl
 * @param {any} $scope
 * @param {any} $jgHttp
 */
function SystemActionCreateCtrl($scope, $jgHttp) {
    const cName = "systemaction";
    $scope.wait = true;
    $scope.systemAction = {};

    function init() {
        $jgHttp.getData(`${apiUrl}/${cName}/get/${id}`, (s => {
            $scope.systemAction = s.data;
            $scope.wait = false;
        }), (e => {
            $scope.wait = false;
        }));
    }
    init();

    $scope.save = () => {
        $jgHttp.postData(`${apiUrl}/${cName}/save`, $scope.systemAction, (s => {
            if (s.isSuccess) {
                window.location = `/admin/${cName}/list`;
            }
        }));
    };
}