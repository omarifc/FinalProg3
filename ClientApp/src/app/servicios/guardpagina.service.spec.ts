import { TestBed } from '@angular/core/testing';

import { GuardpaginaService } from './guardpagina.service';

describe('GuardpaginaService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GuardpaginaService = TestBed.get(GuardpaginaService);
    expect(service).toBeTruthy();
  });
});
