namespace win_rcmts;

public interface ITool {

    public void run(string param);

    public MenuType[] GetMenuTypes();

    public string getMenuName();

    public string getDirect();

    public string getIconPath();

}