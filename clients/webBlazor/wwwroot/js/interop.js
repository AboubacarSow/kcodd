window.scrollToBottom = (elementId) => {
    const el = document.getElementById(elementId);
    if (el) el.scrollTop = el.scrollHeight;
};


window.getTheme = () => {
    return localStorage.getItem('ra-theme') ?? 'light';
};

window.storeResult= (key, value) => {
    var existing = localStorage.getItem(key);
    localStorage.setItem(key, value);
};
window.getResults = (key) => {
    return localStorage.getItem(key);
};

window.clearResults = (key) => {
    localStorage.removeItem(key);
}
window.setTheme = (theme) => {
    localStorage.setItem('ra-theme', theme);
    document.documentElement.setAttribute('data-theme', theme);
};
 
(function () {
    const saved = localStorage.getItem('ra-theme');
    if (saved) document.documentElement.setAttribute('data-theme', saved);
})();