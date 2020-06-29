app.controller("QuizCtrl", QuizCtrl);

function QuizCtrl($scope, $jgHttp) {
    
    $scope.quizList = [];
    $jgHttp.getData(`${apiUrl}/quiz`, function (result) {
        $scope.quizList = result.data;
    }, function (err) {
        
    });

}