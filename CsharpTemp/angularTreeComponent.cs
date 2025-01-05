import { Component, Input } from '@angular/core';
import { TreeNode } from './tree.service';

@Component({
  selector: 'app-tree-node',
  template: `
    <div class="tree-node">
      <div class="node-content">
        <span>{{ node.userId }}</span>
      </div>
      <div class="children">
        <app-tree-node *ngIf="node.leftChild" [node]="node.leftChild"></app-tree-node>
        <app-tree-node *ngIf="node.rightChild" [node]="node.rightChild"></app-tree-node>
      </div>
    </div>
  `,
  styles: [
    `
      .tree-node {
        text-align: center;
        margin-top: 20px;
      }

      .node-content {
        display: inline-block;
        padding: 10px;
        background-color: #007bff;
        color: white;
        border-radius: 50%;
        font-weight: bold;
      }

      .children {
        display: flex;
        justify-content: center;
        margin-top: 10px;
      }

      .children app-tree-node {
        margin: 0 10px;
      }
    `,
  ],
})
export class TreeNodeComponent {
  @Input() node!: TreeNode;
}
