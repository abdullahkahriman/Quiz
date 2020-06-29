var app = angular.module('quizApp', ['ngAnimate']);

const apiUrl = 'https://localhost:44376/api';
const keyToken = 'token';

/**
 * Get local storage find by key
 * @param {any} key
 */
function getLS(key) {
    return localStorage.getItem(key);
}

/**
 * Set local storage
 * @param {any} key
 * @param {any} val
 */
function setLS(key, val) {
    localStorage.setItem(key, val);
}

/**
 * Get token value */
function getToken() {
    getLS(keyToken);
}

/**
 * Set token value
 * @param {any} val
 */
function setToken(val) {
    setLS(keyToken, val);
}

/**
 * Redirect user logged in */
function redirectSignIn() {
    if (!getToken()) {
        window.location = '/account/signin';
    }
}