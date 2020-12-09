import { ShortenUsernamePipe } from './shorten-username.pipe';

describe('ShortenUsernamePipe', () => {
  it('create an instance', () => {
    const pipe = new ShortenUsernamePipe();
    expect(pipe).toBeTruthy();
  });
});
