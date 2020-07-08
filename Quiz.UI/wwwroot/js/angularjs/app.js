var app = angular.module('quizApp', ['ngAnimate']);

const apiUrl = 'https://localhost:44376/api';
const keyToken = 'token';

/**
 * Get local storage find by key
 * @param {any} key
 */
function getLS(key) {
    const val = localStorage.getItem(key);
    return val;
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
 * delete local storage
 * @param {any} key
 */
function delLS(key) {
    localStorage.removeItem(key);
}

/**
 * Get token value */
function getToken() {
    return getLS(keyToken);
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

/** User sign out */
function signOut() {
    delLS(keyToken);
    redirectSignIn();
}
