
window.getScrollLeft = (id) => {
    const el = document.getElementById(id);
    return el ? el.scrollLeft : 0;
};

window.setScrollLeft = (id, value) => {
    const el = document.getElementById(id);
    if (el) el.scrollLeft = value;
};
window.scrollDayIntoView = (id) => {
    const el = document.getElementById(id);
    if (el) {
        el.scrollIntoView({ behavior: 'smooth', inline: 'center', block: 'nearest' });
    }
};