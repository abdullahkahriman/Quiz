//const signUpButton = document.getElementById('signUp');
//const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');

//signUpButton.addEventListener('click', () => {
//	container.classList.add("right-panel-active");
//});

//signInButton.addEventListener('click', () => {
//	container.classList.remove("right-panel-active");
//});

function setActive() {
	const path = getPathName();
	if (path.toLowerCase().indexOf("signup") !== -1) {
		container.classList.add("right-panel-active");
	}
}

setActive();

function getPathName() {
	return window.location.pathname;
}