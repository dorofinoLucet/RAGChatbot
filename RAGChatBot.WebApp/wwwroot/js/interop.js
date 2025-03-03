function copyToClipboard(button) {
    var codeBlock = button.nextElementSibling.querySelector("code");
    var textArea = document.createElement("textarea");
    textArea.value = codeBlock.innerText;
    document.body.appendChild(textArea);
    textArea.select();
    document.execCommand("copy");
    document.body.removeChild(textArea);

    button.innerHTML = "<i class='fas fa-check'></i> Copied!";
    setTimeout(() => {
        button.innerHTML = "<i class='fas fa-copy'></i>";
    }, 2000);
}

window.addEventListener("scroll", function () {
    document.querySelectorAll(".copy-button").forEach(button => {
        let parent = button.closest(".code-container");
        let parentRect = parent.getBoundingClientRect();
        let buttonHeight = button.offsetHeight;
        let parentHeight = parent.clientHeight;

        if (parentRect.top < 20 && parentRect.bottom > buttonHeight + 20) {
            button.style.position = "absolute";
            let scrollPosition = Math.min(window.scrollY - parent.offsetTop + 10, parentHeight - buttonHeight - 10);
            button.style.top = `${scrollPosition}px`;
        } else {
            button.style.position = "absolute";
            button.style.top = "10px";
        }
    });
});

window.scrollChatToBottom = (containerId) => {
    const container = document.getElementById(containerId);
    if (container) {
        container.scrollTop = container.scrollHeight;
    }
};


