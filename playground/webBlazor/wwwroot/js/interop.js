window.scrollTop = (elementId) => {
    const el = document.getElementById(elementId);
    if(el){
        el.scrollTo({
             top: 0,
             behavior: 'smooth' 
            });
    }
}
window.initialiseButton = ()=>{

    const outputArea = document.getElementById('outputPane');
    const scrollTopBtn = document.querySelector('.scroll-top-btn');

    const toggleBtn =()=>{
        if(!outputArea || !scrollTopBtn)
            return;

        const hasScrolled = outputArea.scrollTop > 120;
        const hasScroollableContent = outputArea.scrollHeight > outputArea.clientHeight;

        if(hasScrollableContent && hasScrolled){
            scrollTopBtn.classList.add('visible');
        } else {
            scrollTopBtn.classList.remove('visible');
        }
    }
    
    outputArea.addEventListener('scroll', toggleBtn);

    toggleBtn();
}

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