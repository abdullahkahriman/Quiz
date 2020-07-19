app.controller("QuizListCtrl", QuizListCtrl);
app.controller("QuizCreateCtrl", QuizCreateCtrl);

/**
 * Quiz List Ctrl
 * @param {any} $scope
 * @param {any} $jgHttp
 */
function QuizListCtrl($scope, $jgHttp) {
    const cName = "quiz";
    $scope.wait = true;
    $scope.quizList = [];

    function init() {
        $jgHttp.getData(`${apiUrl}/${cName}/getAdmin`, (s => {
            $scope.quizList = s.data;
            $scope.wait = false;
        }), (e => {
            $scope.wait = false;
        }));
    }
    init();

    /**
    * delete user
    * @param {any} id
    */
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

    /**
     * delete object from quiz list
     * @param {any} id
     */
    function removeByID(id) {
        var arr = $scope.quizList;
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
 * Quiz Create Ctrl
 * @param {any} $scope
 * @param {any} $jgHttp
 */
function QuizCreateCtrl($scope, $jgHttp) {
    const cName = "quiz";
    $scope.wait = true;
    $scope.quiz = {};

    function init() {
        $jgHttp.getData(`${apiUrl}/${cName}/getAdmin/${id}`, (s => {
            $scope.quiz = s.data;
            if (s.data.questionAnswers == null || s.data.questionAnswers.length == 0) {
                $scope.quiz.questionAnswers = [];
                for (var i = 0; i < 4; i++)
                    $scope.quiz.questionAnswers.push({});
            }
            $scope.wait = false;
        }), (e => {
            $scope.wait = false;
        }));
    }
    init();

    /** add answer */
    $scope.addAnswer = () => {
        if (!$scope.quiz.questionAnswers || $scope.quiz.questionAnswers.length == 0)
            $scope.quiz.questionAnswers = [];

        if ($scope.quiz.questionAnswers.length < 5) {
            const obj = {};
            $scope.quiz.questionAnswers.push(obj);
        }
    }

    /** save */
    $scope.save = () => {
        $scope.model = $scope.quiz;

        for (var i = 0; i < $scope.quiz.questionAnswers.length; i++) {
            $scope.model.questionAnswers[i].questionID = $scope.quiz.id;
        }

        $jgHttp.postData(`${apiUrl}/${cName}/save`, $scope.model, (s => {
            if (s.isSuccess) {
                window.location = `/admin/${cName}/list`;
            }
        }));
    }

}