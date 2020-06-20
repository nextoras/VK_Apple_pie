export enum ViewName {
    OnBoarding = 'OnBoarding',
    GetSizes = 'GetSizes',
    Catalog = 'Catalog'
}

export enum PanelName {
    SelectCloth = "SelectCloth",
    SizeCloth = "SizeCloth",
    BuyCloth = 'BuyCloth',
    Upload = 'Upload',
    All = 'All',
    Selected = 'Selected'
}

export enum ModalName {
    SelectCloth = "ModalSelectCloth",
    SizeCloth = "ModalSizeCloth",
    BuyCloth = 'ModalBuyCloth',
}

export const Views = ['OnBoarding', 'GetSizes', 'Catalog']
export const PanelsByView = {
    OnBoarding: ['SelectCloth', 'SizeCloth', 'BuyCloth'],
    GetSizes: ['Upload'],
    Catalog: ['All', 'Selected']
}
