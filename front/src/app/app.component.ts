import {
  Component,
  inject,
} from "@angular/core";
import { RouterModule } from "@angular/router";
import { SplitterModule } from 'primeng/splitter';
import { ToolbarModule } from 'primeng/toolbar';
import { PanelMenuComponent } from "./shared/ui/panel-menu/panel-menu.component";
import { BasketService } from "./basket/data-access/basket.service";
import { BadgeModule } from 'primeng/badge';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { BasketComponent } from "./basket/features/basket/basket.component";
import { ButtonModule } from 'primeng/button';

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
  standalone: true,
  imports: [RouterModule, SplitterModule, ToolbarModule, PanelMenuComponent,BadgeModule,OverlayPanelModule, BasketComponent,ButtonModule],
})
export class AppComponent {
  public readonly basketService = inject(BasketService);
  title = "ALTEN SHOP";
}
