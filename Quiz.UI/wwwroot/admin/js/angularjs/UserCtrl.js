app.controller("UserListCtrl", UserListCtrl);
app.controller("UserCreateCtrl", UserCreateCtrl);

/**
 * User List Ctrl
 * @param {any} $scope
 * @param {any} $jgHttp
 */
function UserListCtrl($scope, $jgHttp) {
    const cName = "user";
    $scope.wait = true;
    $scope.userList = [];

    function init() {
        $jgHttp.getData(`${apiUrl}/${cName}/get`, (s => {
            $scope.userList = s.data;
            $scope.wait = false;
        }), (e => {
            $scope.wait = false;
        }));
    }
    init();
}

/**
 * User Create Ctrl
 * @param {any} $scope
 * @param {any} $jgHttp
 */
function UserCreateCtrl($scope, $jgHttp) {
    const cName = "user";
    $scope.wait = true;
    $scope.user = {};

    function init() {
        if (id > 0) {
            $jgHttp.getData(`${apiUrl}/${cName}/get/${id}`, (s => {
                $scope.user = s.data;
                $scope.wait = false;
            }), (e => {
                $scope.wait = false;
            }));
        } else {
            $scope.wait = false;
        }
    }
    init();

    $scope.save = () => {
        $scope.model = $scope.user;

        $scope.model.userRoles = [];

        for (var i = 0; i < $scope.model.roles.length; i++) {
            if ($scope.model.roles[i].checked == true) {
                $scope.model.userRoles.push({
                    roleID: $scope.model.roles[i].id
                });
            }
        }
       
        $jgHttp.postData(`${apiUrl}/${cName}/save`, $scope.model, (s => {
            if (s.isSuccess) {
                window.location = `/admin/${cName}/list`;
            }
        }));
    };
}