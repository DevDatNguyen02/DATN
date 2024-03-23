import { HotelMarriotterTemplatePage } from './app.po';

describe('HotelMarriotter App', function() {
  let page: HotelMarriotterTemplatePage;

  beforeEach(() => {
    page = new HotelMarriotterTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
