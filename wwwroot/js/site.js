function toggleSidebar() {

    const sidebar = document.getElementById("sidebar");
    const mainArea = document.querySelector(".main-area");

    if (window.innerWidth <= 992) {

        sidebar.classList.toggle("mobile-open");

        return;
    }

    sidebar.classList.toggle("collapsed");

    mainArea.classList.toggle("sidebar-collapsed");
}