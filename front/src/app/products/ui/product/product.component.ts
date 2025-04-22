import { CommonModule } from '@angular/common';
import { Component, input, output } from '@angular/core';
import { Product } from 'app/products/data-access/product.model';
import { TagModule } from 'primeng/tag';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { ImageModule } from 'primeng/image';
@Component({
  selector: 'app-product',
  standalone: true,
  imports: [TagModule, CommonModule, RatingModule, FormsModule, ButtonModule, ImageModule],
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent {
product = input.required<Product>();
cartClick = output<Product>();

getSeverity (product: Product) {
  switch (product.inventoryStatus) {
      case 'INSTOCK':
          return 'success';

      case 'LOWSTOCK':
          return 'warning';

      case 'OUTOFSTOCK':
          return 'danger';

      default:
          return 'danger';
  }
};

//emits the product when the cart button is clicked
onCartClick()
{
  this.cartClick.emit(this.product());
}

}
