app.controller("RoleListCtrl", RoleListCtrl);
app.controller("RoleCreateCtrl", RoleCreateCtrl);

/**
 * Role List Ctrl
 * @param {any} $scope
 * @param {any} $jgHttp
 */
function RoleListCtrl($scope, $jgHttp) {
    const cName = "role";
    $scope.wait = true;
    $scope.roleList = [];

    function init() {
        $jgHttp.getData(`${apiUrl}/${cName}/get`, (s => {
            $scope.roleList = s.data;
            $scope.wait = false;
        }), (e => {
            $scope.wait = false;
        }));
    }
    init();
}

/**
 * Role Create Ctrl
 * @param {any} $scope
 * @param {any} $jgHttp
 */
function RoleCreateCtrl($scope, $jgHttp) {
    const cName = "role";
    $scope.wait = true;
    $scope.role = {};

    function init() {
        $jgHttp.getData(`${apiUrl}/${cName}/get/${id}`, (s => {
            $scope.role = s.data;
            $scope.wait = false;
        }), (e => {
            $scope.wait = false;
        }));
    }
    init();

    $scope.save = () => {
        $scope.model = $scope.role;
        $scope.model.roleSystemActions = [];

        for (var i = 0; i < $scope.role.systemActions.length; i++) {
            if ($scope.role.systemActions[i].checked == true) {
                $scope.model.roleSystemActions.push({
                    roleID: $scope.role.id,
                    systemActionID: $scope.role.systemActions[i].id
                })
            }
        }

        $jgHttp.postData(`${apiUrl}/${cName}/save`, $scope.model, (s => {
            if (s.isSuccess) {
                window.location = `/admin/${cName}/list`;
            }
        }));
    };
}