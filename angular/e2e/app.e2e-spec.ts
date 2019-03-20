import { MongosTemplatePage } from './app.po';

describe('Mongos App', function() {
  let page: MongosTemplatePage;

  beforeEach(() => {
    page = new MongosTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
