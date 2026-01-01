const icons = [
  'fa-cart-shopping',
  'fa-laptop',
  'fa-shirt',
  'fa-couch',
  'fa-gem',
  'fa-pen',
  'fa-box',
  'fa-mobile-screen',
  'fa-desktop',
  'fa-headphones',
  'fa-keyboard',
  'fa-chair',
  'fa-bag-shopping',
  'fa-mug-hot',
  'fa-watch-smart',
  'fa-tv',
  'fa-camera',
  'fa-print',
  'fa-microchip'
];


const container = document.querySelector(".bg-icons");

for (let i = 0; i < 40; i++) {
    const icon = document.createElement("i");
    icon.className = `fas ${icons[Math.floor(Math.random() * icons.length)]} bg-icon`;

    icon.style.left = Math.random() * 100 + "vw";
    icon.style.animationDuration = (10 + Math.random() * 15) + "s";
    icon.style.animationDelay = Math.random() * 10 + "s";
    icon.style.fontSize = (16 + Math.random() * 15) + "px";
    icon.style.opacity = Math.random() * 0.4 + 0.1;

    container.appendChild(icon);
}
