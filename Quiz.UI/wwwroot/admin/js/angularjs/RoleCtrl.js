app.controller("RoleListCtrl", RoleListCtrl);
app.controller("RoleCreateCtrl", RoleCreateCtrl);

/**
 * Role List Ctrl
 * @param {any} $scope
 * @param {any} $jgHttp
 */
function RoleListCtrl($scope, $jgHttp) {
    $scope.wait = true;
    $scope.roleList = [];

    function init() {
        $jgHttp.getData(`${apiUrl}/role/get`, (s => {
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
    $scope.wait = true;
    $scope.role = {};

    function init() {
        $jgHttp.getData(`${apiUrl}/role/get/${id}`, (s => {
            $scope.role = s.data;
            $scope.wait = false;
        }), (e => {
            $scope.wait = false;
        }));
    }
    init();

    $scope.save = () => {
        $jgHttp.postData(`${apiUrl}/role/save`, $scope.role, (s => {
            if (s.isSuccess) {
                window.location = '/admin/role/list';
            }
        }));
    };
}