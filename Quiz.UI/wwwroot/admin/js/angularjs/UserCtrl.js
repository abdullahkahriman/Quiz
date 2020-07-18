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

    $scope.delete = (id) => {
        if (window.confirm("Are you sure?")) {
            $scope.wait = true;
            $jgHttp.postData(`${apiUrl}/${cName}/delete/${id}`, {}, (s => {
                removeByID(id);
                $scope.wait = false;
            }), (e => {
                $scope.wait = false;
            }));
        }
    }

    function removeByID(id) {
        var arr = $scope.userList;
        for (var i = 0; i < arr.length; i++) {
            if (arr[i].id === id) {
                const index = arr.indexOf(arr[i]);
                arr.splice(index, 1);
                break;
            }
        }
    }
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
        $jgHttp.getData(`${apiUrl}/${cName}/get/${id}`, (s => {
            $scope.user = s.data;
            $scope.wait = false;
        }), (e => {
            $scope.wait = false;
        }));
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