import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { BasketService } from 'app/basket/data-access/basket.service';
import { ProductComponent } from 'app/basket/ui/product/product.component';
import { Product } from 'app/products/data-access/product.model';
import { DividerModule } from 'primeng/divider';
import { FieldsetModule } from 'primeng/fieldset';
import { PanelModule } from 'primeng/panel';

@Component({
  selector: 'basket',
  standalone: true,
  imports: [CommonModule, ProductComponent, DividerModule, FieldsetModule, PanelModule],
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.css'
})
export class BasketComponent {
  public readonly basketService = inject(BasketService);

  onRemove(product : Product)
  {
    this.basketService.remove(product);
  }
}
