var app = angular.module('quizApp', []);

const apiUrl = 'https://localhost:44376/api';
const keyToken = 'Token';


function getLS(key) {
    return localStorage.getItem(key);
}
function setLS(key, val) {
    localStorage.setItem(key, val);
}
function getToken() {
    getLS(keyToken);
}
function setToken(val) {
    setLS(keyToken, val);
}
