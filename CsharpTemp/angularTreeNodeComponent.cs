import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface TreeNode {
  id: number;
  userId: string;
  leftChild: TreeNode | null;
  rightChild: TreeNode | null;
}

@Injectable({
  providedIn: 'root',
})
export class TreeService {
  private apiUrl = 'https://your-api-url.com/api/tree'; // Replace with actual API URL

  constructor(private http: HttpClient) {}

  getTree(): Observable<TreeNode> {
    return this.http.get<TreeNode>(this.apiUrl);
  }
}
