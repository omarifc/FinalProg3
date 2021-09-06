import { TestBed } from '@angular/core/testing';

import { FutbolistasService } from './futbolistas.service';

describe('FutbolistasService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FutbolistasService = TestBed.get(FutbolistasService);
    expect(service).toBeTruthy();
  });
});
