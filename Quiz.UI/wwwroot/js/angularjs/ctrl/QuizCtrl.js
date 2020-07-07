app.controller("QuizCtrl", QuizCtrl);

function QuizCtrl($scope, $jgHttp) {

    $scope.quizList = [];
    $scope.opts = [];

    getQuestion();

    $scope.nextQuestion = function (quizID, answerID) {
        setAnswer(quizID, answerID);
    }

    /**
     * Get questions
     * @param {any} quizID
     */
    function getQuestion(quizID) {
        var obj = {};

        if (quizID > 0) {
            obj.id = quizID;
        }

        $jgHttp.postData(`${apiUrl}/quiz/get`, obj, function (result) {
            $scope.quizList = result.data;
        }, function (err) {

        });
    }

    function setAnswer(questionID, answerID) {
        var obj = {
            QuestionID: parseInt(questionID),
            AnswerID: parseInt(answerID),
            UserID: 1
        };
        $jgHttp.postData(`${apiUrl}/quiz/answerthequestion`, obj, function (result) {
            if (result.isSuccess) {
                getQuestion(questionID);
            }
        }, function (err) {

        });
    }

}