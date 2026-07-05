import {ContentContainer} from "@/shared/ui/content-container";
import {LinkButton} from "./link-button/LinkButton.tsx";
import {Button} from "@/shared/ui/button";
import styles from "./Footer.module.css";
import QrCode from "@/shared/assets/images/qr-code.png";
import VkIcon from "@/shared/assets/icons/vk.svg?react";
import TgIcon from "@/shared/assets/icons/tg.svg?react";
import OkIcon from "@/shared/assets/icons/ok.svg?react";
import GlassesIcon from "@/shared/assets/icons/glasses.svg?react";


export const Footer = () => {
    return (
        <footer className={styles.footer}>
            <ContentContainer>
                <div className={styles.content}>

                    <div className={styles.upperContent}>
                        <div className={styles.imageWrapper}>
                            <img className={styles.image} src={QrCode} alt="qr-code"/>
                            <span className={styles.imageText}>
                                Наведите камеру и скачайте бесплатное приложение Wildboars
                            </span>
                        </div>

                        <div className={styles.linkButtonWrapper}>
                            <LinkButton onClick={() => {
                            }}>
                                Об Wildboars / About Wildboars
                            </LinkButton>

                            <LinkButton onClick={() => {
                            }}>
                                Контакты
                            </LinkButton>

                            <LinkButton onClick={() => {
                            }}>
                                Политика обработки данных
                            </LinkButton>
                        </div>

                        <div className={styles.linkButtonWrapper}>
                            <LinkButton onClick={() => {
                            }}>
                                Оплата
                            </LinkButton>

                            <LinkButton onClick={() => {
                            }}>
                                Доставка
                            </LinkButton>

                            <LinkButton onClick={() => {
                            }}>
                                Возврат товаров
                            </LinkButton>
                        </div>

                        <div className={styles.linkButtonWrapper}>
                            <LinkButton onClick={() => {
                            }}>
                                Wildboars Беларусь
                            </LinkButton>

                            <LinkButton onClick={() => {
                            }}>
                                Wildboars Казахстан
                            </LinkButton>

                            <LinkButton onClick={() => {
                            }}>
                                Wildboars Узбекистан
                            </LinkButton>
                        </div>
                    </div>


                    <div className={styles.bottomContent}>
                        <div className={styles.textWrapper}>
                            <a className={styles.textRules}>
                                <span>
                                    1998 – 2026 ООО "Интернет Решения"
                                    (Входит в группу компаний МКПАО "Дикие Кабаны" (Wildboars)).
                                    <br/>
                                    Все права защищены.
                                </span>
                            </a>

                            <span className={styles.textRecommendations}>
                                {"Применяются "}
                                <a className={styles.textRecommendationLink}>
                                    рекомендательные технологии
                                </a>
                            </span>
                        </div>

                        <div className={styles.mediaWrapper}>
                            <div className={styles.socialMedia}>
                                <Button
                                    className={styles.socialMediaButton}
                                    variant={"neutral"}
                                    icon={<VkIcon/>}
                                />

                                <Button
                                    className={styles.socialMediaButton}
                                    variant={"neutral"}
                                    icon={<OkIcon/>}
                                />

                                <Button
                                    className={styles.socialMediaButton}
                                    variant={"neutral"}
                                    icon={<TgIcon/>}
                                />
                            </div>

                            <Button variant={"primary"}
                                    icon={<GlassesIcon/>}>
                                Для слабовидящих
                            </Button>
                        </div>
                    </div>
                </div>

            </ContentContainer>
        </footer>
    );
}