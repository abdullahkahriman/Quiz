app.controller("QuizCtrl", QuizCtrl);

function QuizCtrl($scope, $jgHttp) {

    $scope.quizList = [];
    $scope.opts = [];
    $scope.progressBar = 0;

    //get questions
    getQuestion();

    $scope.nextQuestion = function (quizID, answerID) { 
        if (!quizID || !answerID) {
            alert("No answer selected");
        } else {
            $scope.opts = [];
            setAnswer(quizID, answerID);
        }
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

            //total questions
            let questionTotalCount = result.data.questionTotalCount;
            //total answered question
            let questionTotalAnswered = result.data.questionTotalAnswered;

            $scope.progressBar = (questionTotalAnswered / questionTotalCount) * 100;
        }, function (err) {

        });
    }

    /**
     * Set answer
     * @param {any} questionID
     * @param {any} answerID
     */
    function setAnswer(questionID, answerID) {
        var obj = {
            QuestionID: parseInt(questionID),
            AnswerID: parseInt(answerID),
            UserID: 1
        };
        $jgHttp.postData(`${apiUrl}/quiz/answerthequestion`, obj, function (result) {
            if (result.isSuccess) {
                getQuestion(questionID);
            } else {
                alert(result.message);
            }
        }, function (err) {
            alert(err.message);
        });
    }

}