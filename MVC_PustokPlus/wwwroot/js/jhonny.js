
var urlHost = window.location.origin;

// document.querySelectorAll(".fdisplay").forEach((element) => element.addEventListener("click", function(event) {
//     event.preventDefault();
//     urlMethod = urlHost + event.target.getAttribute("href");
// }));

document.querySelector("#addToCart").addEventListener("click", function (event) {
    event.preventDefault();
    console.log(event.target.getAttribute('href'));
});

function addToCart(id) {
    let instance = document.querySelector("#addToCart" + id);

    console.log(instance.getAttribute("formaction"));
    fetch(urlHost + instance.getAttribute("formaction"))
        .then(res => {
            if (res.ok) {
                // Command: toastr["success"]("Product added")
                alert("added");
                fetch(urlHost + document.getElementById("GetBasket").getAttribute("href"))
                    .then(res => res.json())
                    .then(data => {
                        let temp1 = "";
                        totalCount = 0;
                        totalPrice = 0;

                        for (let index = 0; index < data.length; index++) {
                            const element = data[index];

                            totalCount += element.count;
                            totalPrice += parseFloat(element.costPrice);
                            temp1 +=
                                `<div class=" single-cart-block ">
                                <div class="cart-product">
                                    <a href="Home/product-details" class="image">
                                        <img src="/${element.frontImagePath}" alt="">
                                    </a>
                                    <div class="content">
                                        <h3 class="title">
                                            <a href="Home/product-details">
                                                ${element.name}
                                            </a>
                                        </h3>
                                        <p class="price"><span class="qty">${element.count} ×</span> £${element.sellPrice}</p>
                                        <button class="cross-btn"><i class="fas fa-times"></i></button>
                                    </div>
                                </div>
                            </div>`
                        }

                        let head1 =
                            `<div class="cart-total">
                            <span class="text-number">
                                ${totalCount}
                            </span>
                            <span class="text-item">
                                Shopping Cart
                            </span>
                            <span class="price">
                                £${totalPrice}
                                <i class="fas fa-chevron-down"></i>
                            </span>
                        </div>
                        <div class="cart-dropdown-block">`;

                        let foot1 =
                            `<div class=" single-cart-block ">
                            <div class="btn-block">
                                <a href="Home/cart" class="btn">
                                    View Cart <i class="fas fa-chevron-right"></i>
                                </a>
                                <a href="Home/checkout" class="btn btn--primary">
                                    Check Out <i class="fas fa-chevron-right"></i>
                                </a>
                            </div>
                        </div>
                    </div>`;

                        document.getElementById("basket1").innerHTML = head1 + temp1 + foot1;
                    })
            }
        });
}