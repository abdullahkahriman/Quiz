app.factory('$jgHttp', jgHttp);

/** @ngInject **/
function jgHttp($http) {
    var root = {};

    if (!getLS(keyToken)) {
        redirectSignIn();
    }

    root.getData = function (url, onsuccess, onfail) {
        $http.get(url,
            {
                headers: {
                    'X-Requested-With': 'XMLHttpRequest',
                    "token": getLS(keyToken)
                }
            }).then(function (result) {
                if (result.data.isSuccess) {
                    if (onsuccess) {
                        onsuccess(result.data);
                    }
                    else {
                        alert(result.data.message);
                    }
                } else {
                    if (onfail)
                        onfail(result.data);
                    else
                        alert(result.data.message);
                }

            }, function (error) {
                if (error.status == 401) {
                    alert("Oturumunuz Sonlandı. Tekrar Giriş Yapın!");
                    redirectSignIn();
                }
                else {
                    alert("Hata Var");
                }
            });
    };

    root.postData = function (url, data, onsuccess, onfail) {
        $http.post(url, data,
            {
                headers: {
                    'X-Requested-With': 'XMLHttpRequest',
                    "token": getLS(keyToken)
                }
            }).then(function (result) {
                if (result.data.isSuccess) {
                    if (onsuccess)
                        onsuccess(result.data);
                    else {
                        alert(result.data.message);
                    }
                } else {
                    if (onfail)
                        onfail(result.data);
                    else
                        alert(result.data.message);
                }

            }, function (error) {
                if (error.status == 401) {
                    alert("Oturumunuz Sonlandı. Tekrar Giriş Yapın!");
                    redirectSignIn();
                } else if (error.status == 403) {
                    window.location = '/error/_403';
                } else {
                    alert("Hata Var");
                }
            });

    };

    return root;
}