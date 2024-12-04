

    document.addEventListener('DOMContentLoaded', () => {
        const select = document.querySelector("select");
        const products = document.querySelectorAll("[class^='product-item']"); // All product blocks

        select.addEventListener('change', (event) => {
            const selected = event.target.value;

            products.forEach(product => {
                // Show the entire block if it matches the selection or 'none'
                if (selected === 'none' || product.id === selected) {
                    product.style.display = 'block'; // Make visible
                } else {
                    product.style.display = 'none'; // Hide block
                }
            });
        });
    });

